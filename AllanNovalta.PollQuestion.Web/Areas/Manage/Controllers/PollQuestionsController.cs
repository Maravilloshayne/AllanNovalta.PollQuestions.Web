using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions;
using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PollQuestionsController : Controller
    {

        private readonly DefaultDbContext _context;
        private IHostingEnvironment _env;


        public PollQuestionsController(DefaultDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet, Route("manage/pollquestions/index")]
        [HttpGet, Route("manage/pollquestions")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 2, string keyword = "")
        {

            Page<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion> result = new Page<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion>();
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            IQueryable<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion> pollQuery = (IQueryable<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion>)this._context.PollQuestions;
            if (string.IsNullOrEmpty(keyword) == false)
            {
                pollQuery = pollQuery.Where(u => u.Title.ToLower().Contains(keyword.ToLower()));
            }
            long queryCount = pollQuery.Count();

            int pageCount = (int)Math.Ceiling((decimal)(queryCount / pageSize));
            long mod = (queryCount % pageSize);

            if (mod > 0)
            {
                pageCount = pageCount + 1;
            }

            int skip = (int)(pageSize * (pageIndex - 1));
            List<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion> users = pollQuery.ToList();

            result.Items = users.Skip(skip).Take((int)pageSize).ToList();
            result.PageCount = pageCount;
            result.PageSize = pageSize;
            result.QueryCount = queryCount;
            result.PageIndex = pageIndex;
            result.Keyword = keyword;


            return View(new IndexViewModel()
            {
                PollQuestions = result
            });
        }

        [HttpGet, Route("manage/pollquestions/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("manage/pollquestions/create")]
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion pollquestion = new AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion()
            {
                Id = Guid.NewGuid(),
                Title = model.Title, 
                Description = model.Description,
                Content = model.Content,
                PostExpiry = model.PostExpiry,
                IsPublished = true,
                TemplateName = "pollquestion1"




            };
            this._context.PollQuestions.Add(pollquestion);
            this._context.SaveChanges();
            return View();
        }


        [HttpPost, Route("manage/pollquestions/unpublish")]
        public IActionResult Unpublish(PollQuestionIdViewModel model)
        {
            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == model.Id);
            if (pollquestion != null)
            {
                pollquestion.IsPublished = false;
                this._context.PollQuestions.Update(pollquestion);
                this._context.SaveChanges();
                return Ok();
            }
            return null;
        }

        [HttpPost, Route("manage/pollquestions/publish")]
        public IActionResult Publish(PollQuestionIdViewModel model)
        {
            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == model.Id);

            if (pollquestion != null)
            {
                pollquestion.IsPublished = true;
                this._context.PollQuestions.Update(pollquestion);
                this._context.SaveChanges();
                return Ok();
            }
            return null;
        }



        [HttpGet, Route("/manage/pollquestions/update-title/{pollquestionId}")]
        public IActionResult UpdateTitle(Guid? pollquestionId)
        {
            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == pollquestionId);
            if (pollquestion != null)
            {
                var model = new UpdateTitleViewModel()
                {
                    Id = pollquestion.Id,
                    Description = pollquestion.Description,
                    Title = pollquestion.Title,
                    PostExpiry = pollquestion.PostExpiry,
                    TemplateName = pollquestion.TemplateName

                };
                return View(model);
            }
            return RedirectToAction("Create");
        }

        [HttpPost, Route("/manage/pollquestions/update-title")]
        public IActionResult UpdateTitle(UpdateTitleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == model.Id);

            if (pollquestion != null)
            {
                pollquestion.Title = model.Title;
                pollquestion.Description = model.Description;
                pollquestion.PostExpiry = model.PostExpiry;
                pollquestion.TemplateName = model.TemplateName;

                this._context.PollQuestions.Update(pollquestion);
                this._context.SaveChanges();
            }

            return RedirectToAction("Index");
        }




        [HttpGet, Route("/manage/pollquestions/update-content/{pollquestionId}")]
        public IActionResult UpdateContent(Guid? pollquestionId)
        {
            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == pollquestionId);

            if (pollquestion != null)
            {
                return View(new UpdateContentViewModel()
                {
                    PollQuestionId = pollquestion.Id,
                    Title = pollquestion.Title,
                    Content = pollquestion.Content
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost, Route("/manage/pollquestions/update-content/")]
        public IActionResult UpdateContent(UpdateContentViewModel model)
        {
            var pollquestion = this._context.PollQuestions.FirstOrDefault(p => p.Id == model.PollQuestionId);

            if (pollquestion != null)
            {
                pollquestion.Content = model.Content;
                pollquestion.Timestamp = DateTime.UtcNow;

                this._context.PollQuestions.Update(pollquestion);
                this._context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        [HttpGet, Route("/manage/pollquestions/update-thumbnail/{pollquestionId}")]
        public IActionResult Thumbnail(Guid? pollquestionId)
        {
            return View(new ThumbnailViewModel() { PollQuestionId = pollquestionId });
        }
        [HttpPost, Route("/manage/pollquestions/update-thumbnail")]
        public async Task<IActionResult> Thumbnail(ThumbnailViewModel model)
        {
            //Check file size of the uploaded thumbnail
            //reject if the file is greater than 2mb
            var fileSize = model.Thumbnail.Length;
            if ((fileSize / 1048576.0) > 2)
            {
                ModelState.AddModelError("", "The file you uploaded is too large. Filesize limit is 2mb.");
                return View(model);
            }
            //Check file type of the uploaded thumbnail
            //reject if the file is not a jpeg or png
            if (model.Thumbnail.ContentType != "image/jpeg" && model.Thumbnail.ContentType != "image/png")
            {
                ModelState.AddModelError("", "Please upload a jpeg or png file for the thumbnail.");
                return View(model);
            }
            //Formulate the directory where the file will be saved
            //create the directory if it does not exist
            var dirPath = _env.WebRootPath + "/pollquestions/" + model.PollQuestionId.ToString();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //always name the file thumbnail.png
            var filePath = dirPath + "/thumbnail.png";
            if (model.Thumbnail.Length > 0)
            {
                //Open a file stream to read all the file data into a byte array
                byte[] bytes = await FileBytes(model.Thumbnail.OpenReadStream());
                //load the file into the third party (ImageSharp) Nuget Plugin                
                using (Image<Rgba32> image = Image.Load(bytes))
                {
                    //use the Mutate method to resize the image 150px wide by 150px long
                    image.Mutate(x => x.Resize(150, 150));
                    //save the image into the path formulated earlier
                    image.Save(filePath);
                }
            }
            return RedirectToAction("Thumbnail", new { PollQuestionId = model.PollQuestionId });
        }
        //this method is used to load the file stream into 
        //a byte array
        public async Task<byte[]> FileBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        [HttpGet, Route("/manage/pollquestions/update-banner/{pollquestionId}")]
        public IActionResult Banner(Guid? pollquestionId)
        {
            return View(new BannerViewModel() { PollQuestionId = pollquestionId });
        }
        [HttpPost, Route("/manage/pollquestions/update-banner")]
        public async Task<IActionResult> Banner(BannerViewModel model)
        {
            var fileSize = model.Banner.Length;
            if ((fileSize / 1048576.0) > 5)
            {
                ModelState.AddModelError("", "The file you uploaded is too large. Filesize limit is 5mb.");
                return View(model);
            }
            if (model.Banner.ContentType != "image/jpeg" && model.Banner.ContentType != "image/png")
            {
                ModelState.AddModelError("", "Please upload a jpeg or png file for the banner.");
                return View(model);
            }
            var dirPath = _env.WebRootPath + "/pollquestions/" + model.PollQuestionId.ToString();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var filePath = dirPath + "/banner.png";
            if (model.Banner.Length > 0)
            {
                byte[] bytes = await FileBytes(model.Banner.OpenReadStream());
                using (Image<Rgba32> image = Image.Load(bytes))
                {
                    image.Mutate(x => x.Resize(750, 300));
                    image.Save(filePath);
                }
            }
            return RedirectToAction("Thumbnail", new { PollQuestionId = model.PollQuestionId });
        }


        [HttpPost, Route("/manage/pollquestions/attach-image")]
        public async Task<string> AttachImage(AttachImageViewModel model)
        {
            var fileSize = model.Image.Length;
            if ((fileSize / 1048576.0) > 5)
            {
                return "Error:The file you uploaded is too large. Filesize limit is 5mb.";
            }
            if (model.Image.ContentType != "image/jpeg" && model.Image.ContentType != "image/png")
            {
                return "Error:Please upload a jpeg or png file for the attachment.";
            }
            var dirPath = _env.WebRootPath + "/Images/" + model.PollQuestionId.ToString();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var imgUrl = "/content_" + Guid.NewGuid().ToString() + ".png";
            var filePath = dirPath + imgUrl;
            if (model.Image.Length > 0)
            {
                byte[] bytes = await FileBytes(model.Image.OpenReadStream());
                using (Image<Rgba32> image = Image.Load(bytes))
                {
                    //if image wider than 800 px scale to its aspect ratio
                    if (image.Width > 800)
                    {
                        var ratio = 800 / image.Width;
                        image.Mutate(x => x.Resize(800, Convert.ToInt32(image.Height * ratio)));
                    }
                    image.Save(filePath);
                }
            }
            return "OK:/Images/" + model.PollQuestionId.ToString() + "/" + imgUrl;
        }

    }
}