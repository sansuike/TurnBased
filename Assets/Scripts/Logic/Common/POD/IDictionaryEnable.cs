using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 可以转为IDictionary的对象接口
    /// </summary>
    public interface IDictionaryEnable
    {
        /// <summary>
        /// 转为字典
        /// </summary>
        /// <returns></returns>
        IDictionary ToDic();
    }
