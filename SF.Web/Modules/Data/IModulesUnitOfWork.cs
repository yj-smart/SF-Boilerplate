﻿/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： IModulesUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 14:56:44 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.UoW;
using SF.Core.Infrastructure.Modules;
using SF.Data;


namespace SF.Web.Modules.Data
{
    public interface IModulesUnitOfWork : IUnitOfWork
    {
        #region Repository

        IBaseRepository<InstalledModule> Module { get; }

        #endregion
    }
}
