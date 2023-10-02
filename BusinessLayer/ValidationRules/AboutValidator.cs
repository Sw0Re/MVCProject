using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.AboutDetails1).NotEmpty().WithMessage("Hakkımızda Metin Girişi-1'i Boş Geçemezsiniz.");
            RuleFor(x => x.AboutDetails2).NotEmpty().WithMessage("Hakkımızda Metin Girişi-2'i Boş Geçemezsiniz.");
            RuleFor(x => x.AboutImage1).NotEmpty().WithMessage("Hakkımızda Metin Görsel-2'i Boş Geçemezsiniz.");
            RuleFor(x => x.AboutImage2).NotEmpty().WithMessage("Hakkımızda Metin Görsel-2'i Boş Geçemezsiniz.");
         
        }

    }
}
