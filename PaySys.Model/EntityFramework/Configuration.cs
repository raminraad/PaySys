#region
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Arash;
using Bogus;

using PaySys.Model.Entities;
using PaySys.Model.Enums;
using PaySys.Model.Static;
using ValueType = PaySys.Model.Enums.ValueType;
#endregion


namespace PaySys.Model.EntityFramework
{
    internal sealed class Configuration : DbMigrationsConfiguration<PaySysContext>
    {
        private readonly Faker _faker = new Faker("fa");

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PaySysContext context)
        {
            #region Debug
//			if( Debugger.IsAttached == false )
//				Debugger.Launch();
            #endregion


            #region Static Arrays
            PaySysSetting.CurrentYear = 1397;
            PaySysSetting.CurrentMonth = 007;

            var alphabets = new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };

            var contractFields = new List<Tuple<int, bool, string, string, string>>
            {
                Tuple.Create(01, false, alphabets[0], "CA01", "حقـــوق"),
                Tuple.Create(02, true, alphabets[0], "CA02", "فوق العاده شغل"),
                Tuple.Create(03, true, alphabets[0], "CA03", "تسهیلات زندگی"),
                Tuple.Create(04, true, alphabets[0], "CA04", "فوق العاده تعدیل"),
                Tuple.Create(05, true, alphabets[0], "CA05", "عائله مندی"),
                Tuple.Create(06, true, alphabets[0], "CA06", "حق اولاد"),
                Tuple.Create(07, true, alphabets[0], "CA07", "فوق العاده ویژه"),
                Tuple.Create(08, true, alphabets[0], "CA08", "سایر:جذب2"),
                Tuple.Create(09, true, alphabets[0], "CA09", "فوق العاده جذب"),
                Tuple.Create(10, true, alphabets[0], "CA10", "جیره غیر نقدی"),
                Tuple.Create(11, true, alphabets[0], "CA11", "تفاوت گروه ت ش"),
                Tuple.Create(12, true, alphabets[0], "CA12", "سختی کار"),
                Tuple.Create(13, true, alphabets[0], "CA13", "تفاوت حداقل"),
                Tuple.Create(14, true, alphabets[0], "CA14", "حق مسکن"),
                Tuple.Create(15, true, alphabets[0], "CA15", "کارانه"),
                Tuple.Create(16, true, alphabets[0], "CA16", "فوق العاده تلاش"),
                Tuple.Create(17, true, alphabets[0], "CA17", "نوبت کاری"),
                Tuple.Create(18, true, alphabets[0], "CA18", "فوق العاده ذیحسابی"),
                Tuple.Create(19, true, alphabets[0], "CA19", "کارشناسی ارشد"),
                Tuple.Create(20, true, alphabets[0], "CA20", "برجستگی"),
                Tuple.Create(21, true, alphabets[0], "CA21", "گروه تشویقی"),
                Tuple.Create(22, true, alphabets[0], "CA22", "تفاوت ضریب تعدیل"),
                Tuple.Create(23, true, alphabets[0], "CA23", "تاریخ مازاد سی سال خدمت"),
                Tuple.Create(01, false, alphabets[1], "CB01", "حقـــوق"),
                Tuple.Create(02, true, alphabets[1], "CB02", "عائله مندی"),
                Tuple.Create(03, true, alphabets[1], "CB03", "حق اولاد"),
                Tuple.Create(01, false, alphabets[2], "CC01", "حقـــوق"),
                Tuple.Create(02, true, alphabets[2], "CC02", "سنوات خدمت"),
                Tuple.Create(03, true, alphabets[2], "CC03", "فوق العاده تعدیل"),
                Tuple.Create(04, true, alphabets[2], "CC04", "جیره غیر نقدی"),
                Tuple.Create(05, true, alphabets[2], "CC05", "حق اولاد"),
                Tuple.Create(06, true, alphabets[2], "CC06", "تفاوت ضریب تعدیل"),
                Tuple.Create(07, true, alphabets[2], "CC07", "حق مسکن"),
                Tuple.Create(08, true, alphabets[2], "CC08", "هزینه شب کاری"),
                Tuple.Create(09, true, alphabets[2], "CC09", "فوق العاده شغل"),
                Tuple.Create(10, true, alphabets[2], "CC10", "تسهیلات زندگی"),
                Tuple.Create(11, true, alphabets[2], "CC11", "حداقل حقوق"),
                Tuple.Create(12, true, alphabets[2], "CC12", "حق عائله مندی"),
                Tuple.Create(13, true, alphabets[2], "CC13", "نوبت کاری"),
                Tuple.Create(14, true, alphabets[2], "CC14", "سختی کار"),
                Tuple.Create(15, true, alphabets[2], "CC15", "فوق العاده جذب"),
                Tuple.Create(16, true, alphabets[2], "CC16", "فوق العاده 20%"),
                Tuple.Create(17, true, alphabets[2], "CC17", "کارانه"),
                Tuple.Create(18, true, alphabets[2], "CC18", "بن کارگری"),
                Tuple.Create(19, true, alphabets[2], "CC19", "حمایت قضایی"),
                Tuple.Create(20, true, alphabets[2], "CC20", "کارشناسی ارشد"),
                Tuple.Create(21, true, alphabets[2], "CC21", "فوق العاده تشویق"),
                Tuple.Create(22, true, alphabets[2], "CC22", "ایثارگری"),
                Tuple.Create(01, false, alphabets[3], "CD01", "حقـــوق"),
                Tuple.Create(01, false, alphabets[4], "CE01", "حقـــوق"),
            };

            var variables = new List<Tuple<int, string, string>>
            {
                Tuple.Create(01, "V01", "بیمه تکمیلی سهم کارفرما"),
                Tuple.Create(02, "V02", "بیمه تکمیلی سهم پرسنل"),
                Tuple.Create(03, "V03", "تعداد بیمه درمانی تبعی 1"),
                Tuple.Create(04, "V04", "تعداد بیمه درمانی تبعی 2"),
                Tuple.Create(05, "V05", "تعداد بیمه درمانی تبعی 3"),
                Tuple.Create(06, "V06", "شماره حساب بانکی"),
                Tuple.Create(07, "V07", "شماره حساب پس انداز سهم کارفرما"),
                Tuple.Create(08, "V08", "شماره حساب پس انداز سهم پرسنل"),
                Tuple.Create(09, "V09", "کارکرد ماهانه"),
                Tuple.Create(10, "V10", "ساعات اضافه کار"),
                Tuple.Create(11, "V11", "روزهای تعطیل کاری"),
                Tuple.Create(12, "V12", "مساعده اضافه کار"),
                Tuple.Create(13, "V13", "بدهی بازنشستگی"),
                Tuple.Create(14, "V14", "بدهی مقرری ماه اول"),
                Tuple.Create(15, "V15", "بیمه عمر سهم پرسنل"),
            };

            var miscTitlesPayment = new[]
            {
                "پاداش", "عیدی", "حق الزحمه", "تشویقی", "مساعده", "حق العمل", "حسن انجام کار", "هزینه تحصیل", "بورسیه"
            };
            var miscTitlesDebt = new[]
            {
                "جریمه", "توبیخی", "بدهی قبلی", "وام مسکن", "وام ضروری", "کسر کار", "اضافه مرخصی", "خسارت اموال",
                "هزینه های متفرقه"
            };
            var expenseArticleTitles = new[] {"X X X", "X X X", "X X X"};
            var parameterTitles = new Dictionary<string, string>
            {
                {"P01", "بیمه عمر کارفرما"},
                {"P02", "پس انداز سهم کارفرما"},
                {"P03", "بیمه درمانی تبعی 3"},
                {"P04", "بیمه درمانی تبعی 2"},
                {"P05", "بیمه تبعی 1 سهم کارفرما"},
                {"P06", "بیمه تبعی 1 سهم پرسنل"},
                {"P07", "سقف حقوق مشمول بیمه"},
                {"P08", "تقسیم اضافه کار عادی"},
                {"P09", "تقسیم اضافه کار تعطیل"},
                {"P10", "نرخ مالیات متفرقه"},
                {"P11", "نرخ بازنشستگی کارفرما"},
                {"P12", "نرخ بازنشستگی پرسنل"},
                {"P13", "ساعات کار هفتگی"},
                {"P14", "ضریب اضافه کار و تعطیل"},
                {"P16", "نرخ بیمه سهم کارفرما"},
                {"P17", "نرخ بیمه سهم پرسنل"},
                {"P18", "نرخ بیمه بیکاری"},
                {"P19", "نرخ بیمه کارهای سخت"},
                {"P20", "حداقل مزد سال جاری"},
            };
            var mainGroupParameters = new List<Tuple<string, string>>
            {
                Tuple.Create(alphabets[0], "P01"),
                Tuple.Create(alphabets[0], "P02"),
                Tuple.Create(alphabets[0], "P03"),
                Tuple.Create(alphabets[0], "P04"),
                Tuple.Create(alphabets[0], "P05"),
                Tuple.Create(alphabets[0], "P06"),
                Tuple.Create(alphabets[0], "P07"),
                Tuple.Create(alphabets[0], "P08"),
                Tuple.Create(alphabets[0], "P09"),
                Tuple.Create(alphabets[0], "P10"),
                Tuple.Create(alphabets[0], "P11"),
                Tuple.Create(alphabets[0], "P12"),
                Tuple.Create(alphabets[2], "P01"),
                Tuple.Create(alphabets[2], "P02"),
                Tuple.Create(alphabets[2], "P13"),
                Tuple.Create(alphabets[2], "P14"),
                Tuple.Create(alphabets[2], "P10"),
                Tuple.Create(alphabets[2], "P16"),
                Tuple.Create(alphabets[2], "P17"),
                Tuple.Create(alphabets[2], "P18"),
                Tuple.Create(alphabets[2], "P19"),
                Tuple.Create(alphabets[2], "P20"),
                Tuple.Create(alphabets[3], "P01"),
                Tuple.Create(alphabets[3], "P02"),
                Tuple.Create(alphabets[3], "P13"),
                Tuple.Create(alphabets[3], "P14"),
                Tuple.Create(alphabets[3], "P10"),
                Tuple.Create(alphabets[3], "P16"),
                Tuple.Create(alphabets[3], "P17"),
                Tuple.Create(alphabets[3], "P18"),
                Tuple.Create(alphabets[3], "P19"),
                Tuple.Create(alphabets[3], "P20"),
                Tuple.Create(alphabets[4], "P01"),
                Tuple.Create(alphabets[4], "P02"),
                Tuple.Create(alphabets[4], "P13"),
                Tuple.Create(alphabets[4], "P14"),
                Tuple.Create(alphabets[4], "P10"),
                Tuple.Create(alphabets[4], "P16"),
                Tuple.Create(alphabets[4], "P17"),
                Tuple.Create(alphabets[4], "P18"),
                Tuple.Create(alphabets[4], "P19"),
                Tuple.Create(alphabets[4], "P20"),
            };

            var mobilePrefix = new[]
            {
                "0912", "0935", "0919", "0933", "0918"
            };
            var creditCardNoPrefix = new[]
            {
                "627353", "610433", "621986", "621986", "502229", "622106", "627412", "603770", "603770", "627648",
                "603799"
            };
            var cityNames = new[]
            {
                "آبش‌احمد", "آچاچی", "آذرشهر", "آقکند", "اسکو", "اهر", "ایلخچی", "باسمنج", "بخشایش", "بستان‌آباد",
                "بناب", "بناب مرند", "تبریز", "ترک", "تیمورلو (آذرشهر)", "ترکمانچای", "تسوج", "تیکمه‌داش", "جلفا",
                "جوان‌قلعه", "خاروانا", "خامنه", "خداجو", "خسروشاه", "خمارلو", "خواجه", "دوزدوزان", "زرنق", "زنوز",
                "سراب", "سردرود", "سهند", "سیس", "سیه‌رود", "شبستر", "شربیان", "شرفخانه", "شندآباد", "صوفیان",
                "عجب‌شیر", "قره‌آغاج (چاراویماق)", "کشکسرای", "کلوانق", "کلیبر", "کوزه‌کنان", "گوگان", "لیلان",
                "مبارک‌شهر", "مراغه", "مرند", "ملکان", "ممقان", "مهربان", "میانه", "نظرکهریزی", "وایقان", "ورزقان",
                "هادیشهر", "هریس", "هشترود", "هوراند", "یامچی کهنمو "
            };
            var jobTitleAndDescriptions = new Dictionary<string, string>
            {
                {"مهندس برق", "مهندسی برق یکی از مشاغل مهم و کلیدی صنعت برق و مخابرات به شمار می رود."},
                {
                    "مهندس معماری",
                    "اگر از طراحی لذت برده و عاشق ساخت و ساز هم هستید، شغل مهندسی معماری می تواند برای شما ایده آل باشد."
                },
                {
                    "مهندس شیمی",
                    "مهندس شیمی فردی است که یافته های شیمیدان ها را به صورت عملی و کاربردی درآورده و در صنایع مختلف آنها را بکار می گیرد. در واقع مهندسی شیمی، فرآیند بکارگیری علم شیمی است."
                },
                {
                    "مهندس سخت افزار",
                    "مهندس سخت افزار کامپیوتر به تحقیق، طراحی، توسعه، تعمیر و آزمایش تجهیزات کامپیوتر مانند چیپ ها، صفحه مدارها، مونیتورها یا روترها می پردازد."
                },
                {
                    "مهندس هوا فضا",
                    "اگر شما عاشق هواپیما و فضاپیماها بوده و می خواهید برای پیشرفت و توسعه آنها کاری بکنید، شغل مهندسی هوافضا می تواند برای شما مناسب باشد."
                },
                {
                    "مهندس دریا",
                    "به دلیل داشتن مرزهای آبی گسترده و اهمیت بسیار زیاد حمل و نقل آبی در رشد و توسعه کشور، تخصص ها و مشاغل زیادی در حوزه دریایی ایجاد شده است. مهندسی دریا یکی از اصلی ترین آنها می باشد. مهندسی دریا چندین تخصص را در بر می گیرد که هر کدام حیطه وظایف مجزایی دارند."
                },
                {
                    "مهندس کشاورزی",
                    "مهندس کشاورزی فعالیت های مختلفی را انجام می دهد. او به توسعه استفاده از ماشین آلات کشاورزی و پیاده سازی فرآیندهای صحیح و علمی کشاورزی، باغبانی و جنگلداری می پردازد. مهندس کشاورزی باید دارای دانش و مهارت کافی برای کار کردن در صنایع مختلف کشاورزی و یا بخش بازرگانی و تجارت محصولات کشاورزی باشد."
                },
                {
                    "مهندس بهداشت حرفه ای",
                    "بهداشت حرفه‌ای یا سلامت شغلی یا سلامت کار شاخه‌ای است ازعلم بهداشت وعبارتست از شناسائی، ارزیابی و کنترل عوامل زیان آور موجود در محیط کار به همراه یکسری مراقبت های بهداشتی درمانی به منظور سالم سازی محیط کار و حفظ سلامت نیروی کار. بهداشت حرفه‌ای ترکیبی از علوم پزشکی ومهندسی می‌باشد."
                },
                {
                    "مهندس عمران",
                    "به عنوان مهندس عمران، شما به طراحی و مدیریت انواع پروژه های ساختمانی از تعمیر پل گرفته تا ساخت یک استادیوم ورزشی جدید می پردازید. برای مهندسی عمران باید در ریاضیات و مهارت های فناوری اطلاعات, عالی باشید. همچنین باید بتوانید به خوبی طرح ها و ایده های خود را برای دیگران بیان کرده و در نتیجه باید مهارت های ارتباطی بسیار خوبی داشته باشید. برای دست یابی به شغل مهندسی عمران باید مدارک معتبر دانشگاهی کسب نمایید."
                },
                {
                    "مهندس صنایع",
                    "در بازار رقابتی امروز، شرکت ها و سازمان ها برای بقا و رسیدن به اهداف خود باید از منابع موجود خود اعم از مالی و غیرمالی به صورت بهینه استفاده کنند تا هزینه های خود را به حداقل برسانند. علاوه بر این موارد تلاش در جهت افزایش کیفیت محصولات و خدمات در کنار بکارگیری نوآوری های مختلف، در موفقیت آنها بسیار موثر است. مهندسی صنایع ابزاری اساسی و موثر است که به مدیران و صاحبان شرکت ها و سازمان ها در انجام موارد فوق یاری می رساند."
                },
                {
                    "مهندس نفت",
                    "مهندس نفت روش های استخراج نفت و گاز از زیر زمین را طراحی کرده و توسعه می دهد.محل کار مهندس نفت در دفاتر کاری، پالایشگاه ها، محل های حفاری و یا آزمایشگاه های تحقیقاتی می باشد."
                },
                {
                    "مهندس رباتیک",
                    "مهندس رباتیک فردی است که پاسخگوی  نیاز صنعت در تحقیق و توسعه، طراحی، تولید، نگهداری و تعمیرات ربات ها می باشد."
                },
                {
                    "مهندس معدن",
                    "مهندسی معدن مجموعه علوم، روش ها و فنونی است که از اکتشاف یک معدن آغاز و تا فرآوری آن ادامه دارد. البته معدن باید از نظر اقتصادی ارزش و صرفه کافی را داشته باشد تا بتوان کار اکتشاف و استخراج (که فرآیندهایی پرهزینه هستند) را در آن انجام داد. از آنجا که از سالیان بسیار قبل، انسان ها به دنبال کشف طلا و برخی از مواد معدنی ارزشمند بوده اند، شغل مهندسی معدن دارای قدمت زیادی است."
                },
                {
                    "مهندسی پزشکی",
                    "مهندسی پزشکی کاربرد علوم مهندسی در حوزه پزشکی برای تشخیص و درمان بیماری ها است. حوزه مهندسی پزشکی به دنبال برطرف کردن نیازهای پزشکی در زمینه طراحی، ساخت و نگهداری تجهیزات و ابزارهای پزشکی برای کاربردهای پیشگیری، تشخیص و درمان بیماریها به کمک علوم مهندسی است."
                },
                {
                    "مهندس بهداشت محیط",
                    "مهندسی بهداشت محیط -بهداشت محیط کنترل همه عواملی است که اثر سویی بر پایدار ماندن سلامت انسان می‌گذارند. این شامل بیماری‌های زیادی می‌شود که از طریق آب، هوا، مواد غذایی و بسیاری از عوامل محیطی دیگر سلامت انسان را تهدید می‌کنند. برای رسیدن به این هدف، بهره‌گیری اصول مهندسی و دانش زیست‌محیطی به منظور کنترل، اصلاح و بهبود عوامل فیزیکی، شیمیایی و بیولوژیک محیط جهت حفظ و ارتقاء سلامتی و رفاه و آسایش انسان ضرورت می‌یابد."
                },
                {
                    "مهندس راه آهن",
                    "حمل و نقل یکی از عوامل اصلی و تعیین کننده در دنیای امروز است که در اقتصاد، فرهنگ و در همه شئون مهندس راه آهن – اجتماعی جوامع، نقش چشمگیر و حیاتی دارد. در میان انواع حمل ‌و نقل، حمل‌ و نقل ریلی از مزیت ‌های بسیاری برخوردار است و کشورهای صنعتی و نیمه‌ صنعتی از گذشته های دور به این پدیده پرارزش پرداخته و شبکه حمل و نقل خود را با حمل و نقل ریلی تجهیز کرده‌اند. تاجایی که امروزه خیلی از کشورها به مرحله‌ای رسیده‌اند که چندان به دنبال توسعه کمّی نیستند بلکه به دنبال هماهنگ کردن و هم سو کردن صنعت حمل و نقل ریلی با دیگر پدیده‌ های علمی و صنعتی پیشرو مانند الکترونیک و سیستم ‌های ارتباط جمعی می‌ باشند. در واقع هدف آن ها رسیدن سریع از مبدا به مقصد و امکان جابجایی پرحجم مسافر و کالا است."
                },
                {
                    "مهندس مکانیک",
                    "مهندس مکانیک، اصول اساسی نیرو، انرژی، حرکت و گرما را می‌آموزد و با دانش تخصصی خود، سیستم‌های مکانیکی و دستگاه‌ها و فرایندهای گرمایی را طراحی کرده و می‌سازد. مهندس مکانیک گستره وسیعی از دستگاه‌ها، فرآورده‌ها و فرایندها را تولید می‌کند؛ از قالب ساخت سوزن ته‌گرد تا مدل‌سازی حرکت ماهواره‌ها در فضای خارج از جو، موتورها و سیستم‌های کنترل خودرو و هواپیما، نیروگاه‌های الکتریکی، دستگاه‌های پزشکی و اجزا و قطعه‌های گوناگون و…."
                },
                {
                    "مهندس مواد (مهندس متالورژی)",
                    "مهندس مواد دست اندر کار استخراج، عمل آوری، و امتحان موادی است که در تولیدفراورده های گوناگون، از چیپهای کامپیوتری و صفحات تلوزیون گرفته تا فلز بکار رفته در خودروها به کار می روند. مهندس مواد با فلزات، سرامیک ها، مواد پلاستیکی، نیمه هادیها، و ترکیباتی از موادی که به آنها کامپوزیت (مواد مرکب) می‌گویند، برای بوجود آوردن موادی که دارای خصوصیات خاص مکانیکی، الکتریکی و شیمیائی باشند کار می کند. از جمله کارهای مهندس مواد انتخاب مواد برای کاربردهای جدید نیز میباشد."
                },
                {
                    "مهندس پلیمر",
                    "شما به عنوان مهندس پلیمر محصولاتی را طراحی، تولید و یا اصلاح می کنید که در همه جا وجود داشته و همه ما تا حد زیادی به آنها وابسته هستیم و شاید در دنیای امروز زندگی بدون مواد و محصولات پلیمری بسیار سخت باشد. نمونه ای ساده از این محصولات عبارتند از مسواک، تیوب خمیردندان، دمپایی پلاستیکی، بسیاری از لباس ها، ظروف پلاستیکی و ملامینی آشپزخانه، جلد برخی از کتاب ها، بسیاری از قطعات تلویزیون، کامپیوتر و …"
                }
            };
            var groupTitles = new Dictionary<string, List<string>>
            {
                {
                    "استخدام کشــوری", new List<string>
                    {
                        "کارمندان ثابت",
                        "شهـــــــردار"
                    }
                },
                {
                    "بازنشستگـــان", new List<string>
                    {
                        "بازنشستگان",
                        "موظفیــــن"
                    }
                },
                {
                    "مشمولین قانون کار", new List<string>
                    {
                        "کارگران رسمـــــــــی",
                        "کارگران قراردادی فصلی",
                        "کارگران پیمانـــــــی"
                    }
                },
                {
                    "پیمانـــــی", new List<string>
                    {
                        "پیمانـی اداری",
                        "پیمانی خدماتی"
                    }
                },
                {
                    "استخدام کشوری تأمین اجتماعی", new List<string>
                    {
                        "رسمی تأمین اجتماعی الف",
                        "رسمی تأمین اجتماعــی ب"
                    }
                }
            };
            var groupColors = new[]
            {
                ColorPallet.Goldenrod, ColorPallet.Teal, ColorPallet.CornflowerBlue, ColorPallet.ForestGreen,
                ColorPallet.Maroon, ColorPallet.Navy
            };
            #endregion


            #region MainGroups & SubGroups
            var seedMainGroups = new List<MainGroup>();
            foreach (var g in groupTitles)
            {
                var indexOfGroup = groupTitles.Keys.ToList().IndexOf(g.Key);
                var newGroupMain = new MainGroup
                {
                    Title = g.Key,
                    Alias = $"{alphabets[indexOfGroup]}",
                    ItemColor = groupColors[indexOfGroup],
                    SubGroups = new List<SubGroup>()
                };
                var indexOfSubGroup = 0;
                foreach (var s in g.Value)
                    newGroupMain.SubGroups.Add(new SubGroup
                    {
                        Alias = $"{alphabets[indexOfGroup]}{++indexOfSubGroup:D2}",
                        Title = s,
                        ItemColor = newGroupMain.ItemColor,
                        Is31 = _faker.Random.Bool(),
                        MainGroup = newGroupMain,
                        WorkshopCode = string.Empty,
                    });
                seedMainGroups.Add(newGroupMain);
            }

            context.MainGroups.AddRange(seedMainGroups);
            #endregion


            #region Employees
            var employeeFaker = new Faker<Employee>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.FName = f.Name.FirstName();
                e.LName = f.Name.LastName();
                e.Address =
                    $"{f.Address.City()} - {f.Address.CitySuffix()} - {f.Address.StreetName()} - پلاک  {f.Address.BuildingNumber()}";
                e.BirthPlace = f.Address.City();
                e.IdCardExportPlace = f.Address.City();
                e.CellNo = $"{f.PickRandom(mobilePrefix)}{f.Phone.PhoneNumber("#######")}";
                e.HomeTel = f.Phone.PhoneNumber();
                e.BirthDate = f.Date.Between(new PersianDate(1330, 1, 1).ToDateTime(),
                    new PersianDate(1370, 12, 29).ToDateTime());
                e.IdCardExportDate = f.Date.Between(new PersianDate(1330, 1, 1).ToDateTime(),
                    new PersianDate(1370, 12, 29).ToDateTime());
                e.DossierNo = $"{f.Random.Number(999999):d6}";
                e.PersonnelCode = $"{f.Random.Number(999999):d6}";
                e.FatherName = f.Name.FirstName();
                e.Sex = f.PickRandom(Enum.GetValues(typeof(Sex)).Cast<Sex>().Where(sex => sex != Sex.Unknown));
                e.PostalCode = $"{f.Random.Number(999999999):d10}";
                e.NationalCardNo = $"{f.Random.Number(999999999):d10}";
                e.IdCardNo = $"{f.Random.Number(999999)}";
            });
            var seedEmployees = employeeFaker.Generate(200);
            context.Employees.AddRange(seedEmployees);
            #endregion


            #region ExpenseArticles
            var seedExpenseArticles = new List<ExpenseArticle>();
            var expenseArticleFaker = new Faker<ExpenseArticle>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Title = f.PickRandom(expenseArticleTitles);
                e.Code = $"{f.Random.Number(99999):D5}";
                e.IsActive = true;
            });
            for (var i = 0; i < 10; i++) seedExpenseArticles.Add(expenseArticleFaker.Generate());

            seedExpenseArticles.AddRange(seedExpenseArticles);
            #endregion


            #region Cities
            var seedCities = new List<City>();
            var cityFaker = new Faker<City>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Title = f.PickRandom(cityNames.Where(s => !seedCities.Select(city => city.Title).Contains(s)));
                e.Distance = f.Random.Int(20, 999);
                e.Percentage = f.Random.Number(100);
            });
            for (var i = 0; i < 50; i++) seedCities.Add(cityFaker.Generate());

            context.Cities.AddRange(seedCities);
            #endregion


            #region Jobs
            var seedJobs = new List<Job>();
            var jobFaker = new Faker<Job>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.JobNo = $"{f.Random.Number(999999):d6}";
                e.ItemColor = f.PickRandom(Enum.GetValues(typeof(ColorPallet)).Cast<ColorPallet>()
                    .Where(x => x != ColorPallet.Unknown));
            });
            foreach (var job in jobTitleAndDescriptions)
            {
                jobFaker.RuleFor(j => j.Title, job.Key).RuleFor(j => j.Description, job.Value);
                seedJobs.Add(jobFaker.Generate());
            }

            context.Jobs.AddRange(seedJobs);
            #endregion


            #region ContractFields
            var seedContractFields = new List<ContractField>();
            foreach (var cf in contractFields)
            {
                seedContractFields.Add(new ContractField
                {
                    Index = cf.Item1,
                    IndexInRetirementReport = cf.Item1,
                    IsEditable = cf.Item2,
                    MainGroup = seedMainGroups.First(g => g.Alias == cf.Item3),
                    Alias = cf.Item4,
                    Title = cf.Item5,
                    Year = PaySysSetting.CurrentYear,
                });
            }

            context.ContractFields.AddRange(seedContractFields);
            #endregion


            #region MiscTitles
            var seedMiscTitles = new List<MiscTitle>();
            foreach (var title in miscTitlesPayment)
                seedMiscTitles.Add(new MiscTitle
                {
                    Title = title,
                    IsPayment = true
                });
            foreach (var title in miscTitlesDebt)
                seedMiscTitles.Add(new MiscTitle
                {
                    Title = title,
                    IsPayment = false
                });

            context.MiscTitles.AddRange(seedMiscTitles);
            #endregion


            #region Miscs
            var miscFaker = new Faker<Misc>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Year = PaySysSetting.CurrentYear;
                e.Month = PaySysSetting.CurrentMonth;
                e.ExpenseArticle = f.PickRandom(seedExpenseArticles);
                e.MiscTitle = f.PickRandom(seedMiscTitles);
            });
            var seedMiscs = new List<Misc>();
            foreach (var mainGroup in seedMainGroups)
            foreach (var subGroup in mainGroup.SubGroups)
                for (var i = 0; i < 10; i++)
                {
                    miscFaker.RuleFor(m => m.SubGroup, subGroup);
                    seedMiscs.Add(miscFaker.Generate());
                }

            context.Miscs.AddRange(seedMiscs);
            #endregion


            #region TaxTables
            var taxTableFaker = new Faker<TaxTable>("fa").StrictMode(false)
                .Rules((f, e) => { e.Month = PaySysSetting.CurrentMonth; });
            var seedTaxTables = new List<TaxTable>();
            foreach (var mainGroup in seedMainGroups)
            foreach (var subGroup in mainGroup.SubGroups)
                for (var i = 1395; i <= PaySysSetting.CurrentYear; i++)
                {
                    taxTableFaker.RuleFor(tt => tt.SubGroup, subGroup).RuleFor(tt => tt.Year, i);
                    seedTaxTables.Add(taxTableFaker.Generate());
                }

            context.TaxTables.AddRange(seedTaxTables);
            var taxRowFaker = new Faker<TaxRow>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.ValueTo = f.Random.Number(20000) * 1000;
                e.Factor = f.Random.Number(10);
            });
            var seedTaxRows = new List<TaxRow>();
            foreach (var taxTable in seedTaxTables)
                for (var i = 0; i < 10; i++)
                {
                    taxRowFaker.RuleFor(row => row.TaxTable, taxTable);
                    seedTaxRows.Add(taxRowFaker.Generate());
                }

            context.TaxRows.AddRange(seedTaxRows);
            #endregion


            #region MissionFormulas
            var missionFormulaFaker = new Faker<MissionFormula>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Month = PaySysSetting.CurrentMonth;
                e.DivideFactor = f.Random.Number(30);
                e.AddFactor = f.Random.Number(250000);
                e.MaxFactor = f.Random.Number(1000000);
                e.PerKmFactor = f.Random.Number(500);
                e.MissionFormulaInvolvedContractFields = new List<MissionFormulaInvolvedContractField>();
            });
            var seedMissionFormulas = new List<MissionFormula>();
            foreach (var mainGroup in seedMainGroups)
            foreach (var subGroup in mainGroup.SubGroups)
                for (var i = 1395; i <= PaySysSetting.CurrentYear; i++)
                {
                    missionFormulaFaker.RuleFor(missionFormula => missionFormula.SubGroup, subGroup)
                        .RuleFor(missionFormula => missionFormula.Year, i);
                    seedMissionFormulas.Add(missionFormulaFaker.Generate());
                }

            foreach (var missionFormula in seedMissionFormulas)
                for (var i = 0; i < 10; i++)
                    if (_faker.Random.Bool())
                    {
                        var item = new MissionFormulaInvolvedContractField();
                        var validContractFields = missionFormula.SubGroup.ContractFields.Where(field =>
                            !missionFormula.MissionFormulaInvolvedContractFields.Select(invFld => invFld.ContractField)
                                .Contains(field)).ToList();
                        if (validContractFields.Any())
                        {
                            item.ContractField = _faker.PickRandom(validContractFields);
                            item.MissionFormula = missionFormula;
                            missionFormula.MissionFormulaInvolvedContractFields.Add(item);
                        }
                    }

            context.MissionFormulas.AddRange(seedMissionFormulas);
            #endregion


            #region HandselFormulaFaker
            var handselFormulaFaker = new Faker<HandselFormula>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Month = PaySysSetting.CurrentMonth;
                e.DaysCount = f.Random.Number(60);
                e.TaxRate = f.Random.Number(10);
                e.Max = f.Random.Number(5000) * 1000;
                e.Min = f.Random.Number(5000) * 1000;
                e.TaxFreeValue = f.Random.Number(5000) * 1000;
            });
            var seedHandselFormulas = new List<HandselFormula>();
            foreach (var mainGroup in seedMainGroups)
            foreach (var subGroup in mainGroup.SubGroups)
                for (var i = 1395; i <= PaySysSetting.CurrentYear; i++)
                {
                    handselFormulaFaker.RuleFor(handselFormula => handselFormula.SubGroup, subGroup)
                        .RuleFor(handselFormula => handselFormula.Year, i);
                    seedHandselFormulas.Add(handselFormulaFaker.Generate());
                }

            context.HandselFormulas.AddRange(seedHandselFormulas);
            #endregion


            #region ParameterTitles
            var seedParameterTitles = new List<ParameterTitle>();
            foreach (var prm in parameterTitles)
                seedParameterTitles.Add(new ParameterTitle()
                {
                    Alias = prm.Key,
                    Title = prm.Value
                });

            context.ParameterTitles.AddRange(seedParameterTitles);
            #endregion


            #region Parameters
            var seedParameters = new List<Parameter>();
            foreach (var prm in mainGroupParameters)
            {
                foreach (var sg in seedMainGroups.First(g => g.Alias == prm.Item1).SubGroups)
                    seedParameters.Add(new Parameter
                    {
                        ParameterTitle = seedParameterTitles.First(t => t.Alias == prm.Item2),
                        Year = PaySysSetting.CurrentYear,
                        Month = PaySysSetting.CurrentMonth,
                        SubGroup = sg,
                        Value = _faker.Random.Number(999999),
                    });
            }

            context.Parameters.AddRange(seedParameters);
            #endregion


            #region ParameterInvolvedContractFields
            var seedParameterInvolvedContractFields = new List<ParameterInvolvedContractField>();
            foreach (var p in seedParameters)
            foreach (var c in p.SubGroup.ContractFields)
                if (_faker.Random.Number(1000) % 3 != 0)
                    seedParameterInvolvedContractFields.Add(new ParameterInvolvedContractField
                    {
                        Parameter = p,
                        ContractField = c
                    });
            context.ParameterInvolvedContractFields.AddRange(seedParameterInvolvedContractFields);
            #endregion


            #region ExpenseArticleOfContractFieldForSubGroups
            var seedExpenseArticleOfContractFieldForSubGroups = new List<ExpenseArticleOfContractFieldForSubGroup>();
            foreach (var mainGroup in seedMainGroups)
            foreach (var subGroup in mainGroup.SubGroups)
            foreach (var contractField in subGroup.ContractFields)
                seedExpenseArticleOfContractFieldForSubGroups.Add(new ExpenseArticleOfContractFieldForSubGroup
                {
                    Month = PaySysSetting.CurrentMonth,
                    ContractField = contractField,
                    ExpenseArticle = _faker.PickRandom(seedExpenseArticles),
                    SubGroup = subGroup
                });

            context.ExpenseArticleOfContractFieldForSubGroups.AddRange(seedExpenseArticleOfContractFieldForSubGroups);
            #endregion


            #region ContractMaster
            var contractMasterFaker = new Faker<ContractMaster>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.Employee = f.PickRandom(seedEmployees);
                e.AccountNoEmp =
                    $"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
                e.AccountNoGov =
                    $"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
                e.ContractNo = $"{f.Random.Number(99999):d5}";
                e.DateEmployment = f.Date.Between(e.Employee.BirthDate.AddYears(20), DateTime.Now);
                e.DateExport = f.Date.Between(e.DateEmployment, DateTime.Now);
                e.DateExecution = f.Date.Between(e.DateExport, DateTime.Now);
                e.EducationStand = f.PickRandom(Enum.GetValues(typeof(EducationStand)).Cast<EducationStand>()
                    .Where(sex => sex != EducationStand.Unknown));
                e.EmploymentType = f.PickRandom(Enum.GetValues(typeof(EmploymentType)).Cast<EmploymentType>()
                    .Where(sex => sex != EmploymentType.Unknown));
                e.HardshipFactor = f.Random.Number(100);
                e.InsuranceNo = $"{f.Random.Number(99999999):d8}";
                e.MaritalStatus = f.PickRandom(Enum.GetValues(typeof(MaritalStatus)).Cast<MaritalStatus>()
                    .Where(sex => sex != MaritalStatus.Unknown));
                e.Job = f.PickRandom(seedJobs);
                e.SacrificeStand = f.PickRandom(Enum.GetValues(typeof(SacrificeStand)).Cast<SacrificeStand>()
                    .Where(sex => sex != SacrificeStand.Unknown));
                e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
            });
            var seedContractMasters = contractMasterFaker.Generate(200);
            context.ContractMasters.AddRange(seedContractMasters);
            #endregion


            #region ContractDetail
            var contractDetailFaker = new Faker<ContractDetail>("fa").StrictMode(false)
                .Rules((f, e) => { e.Value = f.Random.Number(100) * 10000; });
            var seedContractDetails = new List<ContractDetail>();
            foreach (var contMast in seedContractMasters)
            foreach (var grpCntField in seedContractFields.Where(c => c.MainGroup.Equals(contMast.MainGroup)))
            {
                contractDetailFaker.RuleFor(detail => detail.ContractMaster, contMast);
                contractDetailFaker.RuleFor(detail => detail.ContractField, grpCntField);
                var contractDetail = contractDetailFaker.Generate();
                seedContractDetails.Add(contractDetail);
            }

            context.ContractDetails.AddRange(seedContractDetails);
            #endregion


            #region Set CurrentContract for each Employee
            var queryEmpContracts = from cont in seedContractMasters
                group cont by cont.Employee
                into newGroup
                select newGroup;
            foreach (var empContract in queryEmpContracts)
            {
                var lastCnt = empContract.LastOrDefault();
                if (lastCnt != null) lastCnt.IsCurrent = true;
            }
            #endregion


            #region MiscRecharges
            var seedMiscRecharges = new List<MiscRecharge>();
            var miscRechargeFaker = new Faker<MiscRecharge>("fa").StrictMode(false)
                .Rules((f, e) => { e.Value = f.Random.Number(100) * 1000; });
            var firstTenContracts = seedContractMasters.Where(master => master.IsCurrent).Take(10);
            foreach (var contract in firstTenContracts)
            {
                var contractMiscs = contract.SubGroup.Miscs.Take(_faker.Random.Number(contract.SubGroup.Miscs.Count));
                foreach (var misc in contractMiscs)
                {
                    miscRechargeFaker.RuleFor(r => r.Employee, contract.Employee).RuleFor(r => r.Misc, misc);
                    for (var y = 1395; y <= PaySysSetting.CurrentYear; y++)
                    for (var m = 5; m <= 11; m++)
                    {
                        miscRechargeFaker.RuleFor(r => r.Year, y).RuleFor(r => r.Month, m);
                        seedMiscRecharges.Add(miscRechargeFaker);
                    }
                }
            }

            context.MiscRecharges.AddRange(seedMiscRecharges);
            #endregion


            #region Missions
            var seedMissions = new List<Mission>();
            var missionFaker = new Faker<Mission>("fa").StrictMode(false).Rules((f, e) =>
            {
                e.AmountNonResident = f.Random.Number(15);
                e.AmountResident = f.Random.Number(15);
                e.City = f.PickRandom(seedCities);
                e.MissionContractNo = f.Random.Number(99999).ToString("d5");
                e.VehicleType = f.PickRandom(Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>()
                    .Where(v => v != VehicleType.Unknown));
                e.TravelExpense = f.Random.Number(1000) * 1000;
            });

            foreach (var contract in seedContractMasters)
            {
                var startMonth = _faker.Random.Number(9) + 1;
                missionFaker.RuleFor(m => m.ContractMaster, contract);
                missionFaker.RuleFor(m => m.StartDate, _faker.Date.Between(contract.DateEmployment, DateTime.Now));
                missionFaker.RuleFor(m => m.EndDate, (f, m) => f.Date.Between(m.StartDate, DateTime.Now));
                missionFaker.RuleFor(m => m.StartTime, _faker.Date.Past());
                missionFaker.RuleFor(m => m.EndTime, _faker.Date.Past());

                for (var i = 0; i < 10; i++) seedMissions.Add(missionFaker.Generate());
            }

            context.Missions.AddRange(seedMissions);
            #endregion


            #region VariableTitles
            var seedVariables = new List<Variable>();

            foreach (var mainGroup in seedMainGroups)
            foreach (var v in variables)
                seedVariables.Add(new Variable
                {
                    Index = v.Item1,
                    Alias = v.Item2,
                    Title = v.Item3,
                    ValueType =
                        _faker.PickRandom(Enum.GetValues(typeof(ValueType)).Cast<ValueType>()
                            .Where(type => type != ValueType.Unknown)),
                    FromMonth = 1,
                    FromYear = 1397,
                    ToMonth = 12,
                    ToYear = 1397,
                    MainGroup = mainGroup
                });

            context.Variables.AddRange(seedVariables);
            #endregion


            #region VariableValueForEmployees
            var seedVariableValues = new List<VariableValueForEmployee>();
            var variableValueFaker = new Faker<VariableValueForEmployee>("fa").StrictMode(false);

            foreach (var contract in seedContractMasters)
            {
                if (_faker.Random.Bool()) continue;

                foreach (var variable in seedVariables)
                {
                    variableValueFaker.RuleFor(r => r.Employee, contract.Employee).RuleFor(r => r.Variable, variable);
                    for (var y = 1395; y <= PaySysSetting.CurrentYear; y++)
                    for (var m = 5; m <= 11; m++)
                    {
                        variableValueFaker.RuleFor(r => r.Year, y).RuleFor(r => r.Month, m).RuleFor(v => v.Value,
                            (f, v) =>
                            {
                                switch (v.Variable.ValueType)
                                {
                                    case ValueType.Unknown:
                                        return null;
                                    case ValueType.Absolute:
                                    case ValueType.Rial:
                                        return f.Random.Number(100) * 1000;
                                    case ValueType.Percent:
                                        return f.Random.Number(100);
                                    case ValueType.Date:
                                        return f.Date.Past(3, DateTime.Now);
                                    case ValueType.String:
                                        return f.Commerce.ProductAdjective();
                                    default:
                                        return string.Empty;
                                }
                            });
                        seedVariableValues.Add(variableValueFaker);
                    }
                }
            }

            context.VariableValueForEmployees.AddRange(seedVariableValues);
            #endregion


            base.Seed(context);
        }
    }
}