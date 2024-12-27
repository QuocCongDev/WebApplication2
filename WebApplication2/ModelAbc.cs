namespace WebApplication2
{
    public class ModelAbc 
    {
        public int Id { get; set; }
        public string DomainName { get; set; }
        public List<ModelTest1> TestList { get; set; }
        public List<string> names23 { get; set; }
        public ModelTest1 Description { get; set; }
    }

    public class ModelTest1
    {
        public string DomainName { get; set; }
    }

    public class ModelFather
    {
        public string DomainNameMl { get; set; }
    }
    public abstract class DomainRenewRequestHubDtoAbstract<Type> 
            where Type : DomainRenewDomainRequestHubDtoAbstract
    {
        /// <summary>
        /// 支払い方法
        /// </summary>
        public string PaymentType
        {
            get;
            set;
        }

        /// <summary>
        /// コンビニ情報
        /// </summary>
        public DomainRenewUpdatePaymentConveniInfoRequestHubDto ConveniInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 自動更新の支払い方法が設定されているかどうか
        /// </summary>
        public bool HasSetAutoRenewPayment
        {
            get;
            set;
        }

        /// <summary>
        /// 更新対象のドメイン郡
        /// </summary>
        public List<Type> RenewDomains
        {
            get;
            set;
        }

        /// <summary>
        /// ホスティング同時 リコメンドID
        /// </summary>
        public string HostingRecommendId
        {
            get;
            set;
        }

        /// <summary>
        /// ホスティング同時 ドメイン名
        /// </summary>
        public string HostingDomainName
        {
            get; set;
        }

        /// <summary>
        /// GMOポイント
        /// </summary>
        public string GmoPoint
        {
            get;
            set;
        }

        /// <summary>
        /// モバイル・PCを判定するフラグ
        /// </summary>
        public bool IsMobile
        {
            get;
            set;
        }

        public bool Validate()
        {
            var isRequired = CheckRequired();
            var isMaxLength = CheckMaxLength();
            var isRegularExpression = CheckRegularExpression();

            return isRequired && isMaxLength && isRegularExpression;
        }

        private bool CheckRequired()
        {
            var isSuccess = true;
            isSuccess = isSuccess && RenewDomains.All(x => string.IsNullOrEmpty(x.DomainName) == false && string.IsNullOrEmpty(x.Period) == false && string.IsNullOrEmpty(x.CurExpDate) == false);

            return isSuccess;
        }

        private bool CheckMaxLength()
        {
            var isSuccess = true;
            return isSuccess;
        }

        private bool CheckRegularExpression()
        {
            var isSuccess = true;
            return isSuccess;
        }
    }

    public abstract class DomainRenewDomainRequestHubDtoAbstract
    {
        /// <summary>
        /// ドメインID
        /// </summary>
        public string DomainId
        {
            get;
            set;
        }

        /// <summary>
        /// 更新年数
        /// </summary>
        public string Period
        {
            get;
            set;
        }

        /// <summary>
        /// 現在の更新期限日
        /// </summary>
        public string CurExpDate
        {
            get;
            set;
        }

        /// <summary>
        /// 自動更新設定可能かどうか
        /// </summary>
        public bool CanSetAutoRenew
        {
            get;
            set;
        }

        /// <summary>
        /// WHOIS代行設定申し込み済みフラグ
        /// </summary>
        public bool IsAppliedWhoisProxy
        {
            get;
            set;
        }

        /// <summary>
        /// WHOIS代行設定フラグ
        /// </summary>
        public bool IsWhoisProxy
        {
            get;
            set;
        }

        /// <summary>
        /// WHOISメール転送申し込み済みフラグ
        /// </summary>
        public bool IsAppliedWhoisMailFwd
        {
            get;
            set;
        }

        /// <summary>
        /// WHOISメール転送フラグ
        /// </summary>
        public bool IsWhoisMailFwd
        {
            get;
            set;
        }

        /// <summary>
        /// ドメインプロテクション申し込み済みフラグ
        /// </summary>
        public bool IsAppliedDomainProtect
        {
            get;
            set;
        }

        /// <summary>
        /// ドメインプロテクション
        /// </summary>
        public bool IsDomainProtect
        {
            get;
            set;
        }


        /// <summary>
        /// SSL申し込み済みフラグ
        /// </summary>
        public bool IsAppliedSsl
        {
            get;
            set;
        }

        /// <summary>
        /// SSL
        /// </summary>
        public bool IsSsl
        {
            get;
            set;
        }

        /// <summary>
        /// Whois系がプロテクト状態かどうか
        /// </summary>
        public bool IsProtectedWhois
        {
            get;
            set;
        }

        /// <summary>
        /// 自動更新がプロテクト状態かどうか
        /// </summary>
        public bool IsProtectedAutoRenew
        {
            get;
            set;
        }

        /// <summary>
        /// レジストリプレイミアムフラグ
        /// </summary>
        public bool IsRegistryPremium
        {
            get;
            set;
        }

        /// <summary>
        /// クーポンコード
        /// </summary>
        public string CouponCode
        {
            get;
            set;
        }

        /// <summary>
        /// ドメイン名
        /// </summary>
        public string DomainName
        {
            get;
            set;
        }

    }

    public class DomainRenewUpdatePaymentConveniInfoRequestHubDto
    {
        /// <summary>
        /// コンビニコード
        /// </summary>
        public string ConveniCode
        {
            get;
            set;
        }

        /// <summary>
        /// コンビニ名義人
        /// </summary>
        public string UserNameKana
        {
            get;
            set;
        }
    }
    public class DomainRenewUpdateRequestHubDto : DomainRenewRequestHubDtoAbstract<DomainRenewUpdateDomainRequestHubDto>
    {
    }

    public class DomainRenewUpdateDomainRequestHubDto : DomainRenewDomainRequestHubDtoAbstract
    {
        /// <summary>
        /// GMOのでんき
        /// </summary>
        public bool IsElectric
        {
            get;
            set;
        }

        /// <summary>
        /// クーポンを利用する
        /// </summary>
        public string DomainNameMl
        {
            get;
            set;
        }
    }
}
