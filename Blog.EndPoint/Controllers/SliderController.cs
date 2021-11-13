using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Sliders.Create;
using Blog.Application.Commands.Sliders.Delete;
using Blog.Application.Commands.Sliders.Update;
using Blog.Application.Dtos.Sliders;
using Blog.Infrastructure.Services.Files;
using Blog.Shared.Helpers;
using FluentValidation;
using MediatR;
using Blog.EndPoint.Infrastructure.Extensions;
using Blog.EndPoint.Infrastructure.Extensions.Validators;

namespace Blog.EndPoint.Controllers
{

    public class SliderController : BaseController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IValidator _validator;
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public SliderController(IMediator mediator, IValidator validator, IFileService fileService)
        {
            _mediator = mediator;
            _validator = validator;
            _fileService = fileService;
        }
        #endregion

        #region Actions

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSlider(CreateSliderDto model, CancellationToken cancellationToken)
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

                        var directory = _fileService.EnsureExistDirectory($"images",
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
                            $"/images/{CommonHelper.ImageName(path)}";
                    }
                }
            }

            #endregion

            var command = new CreateSliderCommand
            {
                Description = model.Description,
                Image = image,
                Title = model.Title,
                Url = model.Url
            };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("EditSlider")]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto model,CancellationToken cancellationToken)
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

                        var directory = _fileService.EnsureExistDirectory($"images",
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
                            $"/images/{CommonHelper.ImageName(path)}";
                    }
                }
            }

            #endregion
            var command = new UpdateSliderCommand
            {
                Description = model.Description,
                Image = image,
                Title = model.Title,
                Url = model.Url
            };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("RemoveSlider/{id:long}")]

        public async Task<IActionResult> DeleteSlider(long id)
        {
            var result = await _mediator.Send(new DeleteSliderCommand { Id = id });
            return Ok(result);
        }
        #endregion
    }
}
