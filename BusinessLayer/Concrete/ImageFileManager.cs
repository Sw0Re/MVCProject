﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
  public  class ImageFileManager:IImageFileService
    {


        IImageFileDal _ımageFileDal;

        public ImageFileManager(IImageFileDal ımageFileDal)
        {
            this._ımageFileDal = ımageFileDal;
        }

        public void Add(ImageFile imageFile)
        {
            _ımageFileDal.Insert(imageFile);
        }

        public List<ImageFile> GetList()
        {
            return _ımageFileDal.List();
        }
    }
}