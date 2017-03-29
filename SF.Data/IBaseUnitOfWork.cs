﻿/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： IBaseUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 14:56:44 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.EFCore.UoW;
using SF.Data.WorkArea;

namespace SF.Data
{
    public interface IBaseUnitOfWork : IEFCoreUnitOfWork
    {
        #region Work Areas

        IBaseWorkArea BaseWorkArea { get; }

        #endregion
    }
}
