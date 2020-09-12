﻿using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using System;

namespace CleanArchitecture.Core.Entities
{
    public class MissionDocument : BaseEntity, IMissionChildEntity, IDocument, IFile
    {
        public MissionDocument()
        {
        }

        public Mission Mission { get; set; }
        public string MissionId { get; set; }
        public Uri FileUri { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeId { get; set; }
    }
}
