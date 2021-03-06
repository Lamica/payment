﻿using System.Collections.Generic;
using Essensoft.AspNetCore.Payment.WeChatPay.Utility;

namespace Essensoft.AspNetCore.Payment.WeChatPay.Request
{
    /// <summary>
    /// 微信内H5 调起支付
    /// </summary>
    public class WeChatPayJsApiSdkRequest : IWeChatPaySdkRequest
    {
        /// <summary>
        /// 订单详情扩展字符串
        /// </summary>
        public string Package { get; set; }

        #region IWeChatPaySdkRequest Members

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary
            {
                { "package", Package }
            };
            return parameters;
        }

        public void PrimaryHandler(WeChatPayOptions options, WeChatPayDictionary sortedTxtParams)
        {
            sortedTxtParams.Add(WeChatPayConsts.timeStamp, WeChatPayUtility.GetTimeStamp());
            sortedTxtParams.Add(WeChatPayConsts.nonceStr, WeChatPayUtility.GenerateNonceStr());
            sortedTxtParams.Add(WeChatPayConsts.appId, options.AppId);

            sortedTxtParams.Add(WeChatPayConsts.signType, WeChatPayConsts.MD5);
            sortedTxtParams.Add(WeChatPayConsts.paySign, WeChatPaySignature.SignWithKey(sortedTxtParams, options.Key, WeChatPaySignType.MD5));
        }

        #endregion
    }
}
