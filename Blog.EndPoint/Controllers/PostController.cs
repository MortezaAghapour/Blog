using Blog.Application.Commands.Posts.Create;
using Blog.Application.Commands.Posts.Delete;
using Blog.Application.Commands.Posts.Update;
using Blog.Application.Dtos.Posts;
using Blog.EndPoint.Infrastructure.Extensions.Validators;
using Blog.Infrastructure.Services.Files;
using Blog.Shared.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.EndPoint.Controllers
{

    public class PostController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IValidator _validator;
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public PostController(IMediator mediator, IValidator validator, IFileService fileService)
        {
            _mediator = mediator;
            _validator = validator;
            _fileService = fileService;
        }
        #endregion

        #region Actions

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePost(CreatePostDto model, CancellationToken cancellationToken)
        {
            #region Validate Model
            var errors = await _validator.FluentValidate(model, cancellationToken);
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            #endregion

            #region Upload Image  
            var image = string.Empty;
            if (model.Image != null && _fileService.CheckLength(model.Image.Length))
            {
                var imageMimeTypeValues = MimeTypeHelper.GetImageMimeTypeValues().Select(c => c.Value).ToList();
                if (_fileService.CheckMimeTypes(model.Image.ContentType, imageMimeTypeValues))
                {

                    var imageMimeTypeKeys = MimeTypeHelper.GetImageMimeTypeValues().Select(c => c.Key).ToList();
                    if (_fileService.CheckExtension(_fileService.GetExtension(model.Image.FileName.ToLower()),
                        imageMimeTypeKeys))
                    {

                        var directory = _fileService.EnsureExistDirectory($"blogImages",
                            _fileService.WwwrootPath());
                        var fileName = _fileService.GetFileName(model.Image.FileName);
                        var withoutExtension = _fileService.GetFileNameWithoutExtension(fileName);

                        var extension = _fileService.GetFileExtension(fileName);
                        var now = DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
                        var path = _fileService.CombinePath(directory,
                            string.Concat(withoutExtension, now,
                                extension));
                        await _fileService.SaveFile(model.Image, path);


                        image =
                            $"/blogImages/{CommonHelper.ImageName(path)}";
                    }
                }
            }

            #endregion

            var command = new CreatePostCommand
            {
                Author=model.Author,
                CategoryId=model.CategoryId,
                   FullDescription=model.FullDescription,
                   ShortDescription=model.ShortDescription,
                   Slug=model.Slug,
                   Tags=model.Tags,
                Image = image,
                Title = model.Title,
                IsPublish= model.IsPublish
            };
            if (model.IsPublish)
            {
                command.PublishDate = DateTime.Now;
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("EditPost")]
        public async Task<IActionResult> UpdatePost(UpdatePostDto model, CancellationToken cancellationToken)
        {
            #region Validate Model
            var errors = await _validator.FluentValidate(model, cancellationToken);
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            #endregion
            #region Upload Image  
            var image = string.Empty;
            if (model.Image != null && _fileService.CheckLength(model.Image.Length))
            {
                var imageMimeTypeValues = MimeTypeHelper.GetImageMimeTypeValues().Select(c => c.Value).ToList();
                if (_fileService.CheckMimeTypes(model.Image.ContentType, imageMimeTypeValues))
                {

                    var imageMimeTypeKeys = MimeTypeHelper.GetImageMimeTypeValues().Select(c => c.Key).ToList();
                    if (_fileService.CheckExtension(_fileService.GetExtension(model.Image.FileName.ToLower()),
                        imageMimeTypeKeys))
                    {

                        var directory = _fileService.EnsureExistDirectory($"postImages",
                            _fileService.WwwrootPath());
                        var fileName = _fileService.GetFileName(model.Image.FileName);
                        var withoutExtension = _fileService.GetFileNameWithoutExtension(fileName);

                        var extension = _fileService.GetFileExtension(fileName);
                        var now = DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
                        var path = _fileService.CombinePath(directory,
                            string.Concat(withoutExtension, now,
                                extension));
                        await _fileService.SaveFile(model.Image, path);


                        image =
                            $"/postImages/{CommonHelper.ImageName(path)}";
                    }
                }
            }

            #endregion
            var command = new UpdatePostCommand
            {
                Author = model.Author,
                CategoryId = model.CategoryId,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                Slug = model.Slug,
                Tags = model.Tags,
                Image = image,
                Title = model.Title,
                IsPublish = model.IsPublish
            };
            if (model.IsPublish)
            {
                command.PublishDate = DateTime.Now;
            }
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("RemovePost/{id:long}")]

        public async Task<IActionResult> DeletePost(long id)
        {
            var result = await _mediator.Send(new DeletePostCommand { Id = id });
            return Ok(result);
        }
        #endregion
    }
}
