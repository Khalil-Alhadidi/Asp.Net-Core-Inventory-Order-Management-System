using Application.Common.Repositories;
using Application.Features.TodoItemManager.Commands;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ColorManager.Commands
{
    public class CreateColorResult
    {
        public Color? Data { get; set; }
    }

    public class CreateColorRequest : IRequest<CreateColorResult>
    {
        public string? Name { get; init; }
        public string? Code { get; init; }

        public bool? isFav { get; init; }
    }

    public class CreateColorValidator : AbstractValidator<CreateColorRequest>
    {
        public CreateColorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    public class CreateColorHandler : IRequestHandler<CreateColorRequest, CreateColorResult>
    {
        private readonly ICommandRepository<Color> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateColorHandler(
            ICommandRepository<Color> repository,
            IUnitOfWork unitOfWork
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateColorResult> Handle(CreateColorRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Color();
            entity.isFav = request.isFav;

            entity.Name = request.Name;
            entity.Code = request.Code;
         

            await _repository.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return new CreateColorResult
            {
                Data = entity
            };
        }
    }

}
