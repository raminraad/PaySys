#region

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using Arash;
using Bogus;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using ValueType = PaySys.ModelAndBindLib.Model.ValueType;

#endregion

namespace PaySys.ModelAndBindLib.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<PaySysContext>
	{
		private readonly Faker _faker = new Faker( "fa" );

		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed( PaySysContext context )
		{
			#region Debug

//			if( Debugger.IsAttached == false )
//				Debugger.Launch();

			#endregion

			#region Static Arrays

			var contractFields = new[]
			{
				"بیمه تکمیلی سهم دولت",
				"بیمه تبعی یک",
				"بیمه تبعی دو",
				"بیمه مازاد",
				"درصد معاف مالیات",
				"شماره حساب سهم دولت",
				"شماره حساب سهم پرسنل",
				"مبلغ کل بیمه تکمیلی",
				"اضافه کار عادی",
				"اضافه کار جمعه",
				"بازنشستگی گذشته",
				"بیمه عمر",
				"کسور متفرقه",
				"مقرری ماه اول",
				"بدهی اضافه کار",
				"معافیت وام مسکن",
				"مازاد سی سال",
				"پرداختهای متفرقه",
				"مدت پرداخت بروز",
				"سهم بیمه تکمیلی",
				"روزهای کارکرد",
				"ساعات شیفت کاری",
				"بدهی اضافه کار",
				"مدت مرخصی ذخیره",
				"ضریب پایان کار",
				"حق جذب",
				"عائله مندی",
				"سختی کار",
				"خوار و بار",
				"ایاب و ذهاب",
				"فوق العاده اشتغال خارج از كشور",
				"فوق العاده شغل",
				"فوق العاده مخصوص",
				"بدي آب و هوا",
				"تفاوت تطبيق",
				"حق سرپرستي",
				"همطرازي",
				"هزينه سفر"
			};
			var miscTitlesPayment = new[]
			{
				"پاداش",
				"عیدی",
				"حق الزحمه",
				"تشویقی",
				"مساعده",
				"حق العمل",
				"حسن انجام کار",
				"هزینه تحصیل",
				"بورسیه"
			};
			var miscTitlesDebt = new[]
			{
				"جریمه",
				"توبیخی",
				"بدهی قبلی",
				"وام مسکن",
				"وام ضروری",
				"کسر کار",
				"اضافه مرخصی",
				"خسارت اموال",
				"هزینه های متفرقه"
			};
			var expenseArticles = new[]
			{
				"دیون",
				"اداری",
				"پاداشها",
				"درآمدها",
				"بودجه مازاد",
				"هزینه های پرسنل",
				"سرمایه ای",
				"تفاوت تطبيق",
				"مزایا و کمکها",
				"حقوق و دستمزد",
				"هزینه های سری",
				"پرداختهای انتقالی"
			};
			var parameterTitles = new[]
			{
				"درصد مأموریت با بیتوته",
				"درصد مأموریت بدون بیتوته",
				"ضریب نرخ کارکرد",
				"نرخ بیمه حق العمل",
				"ضریب فوق العاده",
				"نرخ مزایای متفرقه",
				"میزان بیمه سنواتی",
				"درصد تعدیل مازاد",
				"نرخ افزایش درآمد",
				"میزان کسور متفرقه",
				"کسر هزار ریال",
				"نرخ کارکرد",
				"ضریب دریافت علی الحساب",
				"درصد معافیت",
				"نرخ تورم",
				"میزان ده درصد",
				"ضریب افزایشی",
				"کاهش بیست درصد"
			};
			var mobilePrefix = new[]
			{
				"0912",
				"0935",
				"0919",
				"0933",
				"0918"
			};
			var creditCardNoPrefix = new[]
			{
				"627353",
				"610433",
				"621986",
				"621986",
				"502229",
				"622106",
				"627412",
				"603770",
				"603770",
				"627648",
				"603799"
			};
			var cityNames = new[]
			{
				"آبش‌احمد",
				"آچاچی",
				"آذرشهر",
				"آقکند",
				"اسکو",
				"اهر",
				"ایلخچی",
				"باسمنج",
				"بخشایش",
				"بستان‌آباد",
				"بناب",
				"بناب مرند",
				"تبریز",
				"ترک",
				"تیمورلو (آذرشهر)",
				"ترکمانچای",
				"تسوج",
				"تیکمه‌داش",
				"جلفا",
				"جوان‌قلعه",
				"خاروانا",
				"خامنه",
				"خداجو",
				"خسروشاه",
				"خمارلو",
				"خواجه",
				"دوزدوزان",
				"زرنق",
				"زنوز",
				"سراب",
				"سردرود",
				"سهند",
				"سیس",
				"سیه‌رود",
				"شبستر",
				"شربیان",
				"شرفخانه",
				"شندآباد",
				"صوفیان",
				"عجب‌شیر",
				"قره‌آغاج (چاراویماق)",
				"کشکسرای",
				"کلوانق",
				"کلیبر",
				"کوزه‌کنان",
				"گوگان",
				"لیلان",
				"مبارک‌شهر",
				"مراغه",
				"مرند",
				"ملکان",
				"ممقان",
				"مهربان",
				"میانه",
				"نظرکهریزی",
				"وایقان",
				"ورزقان",
				"هادیشهر",
				"هریس",
				"هشترود",
				"هوراند",
				"یامچی کهنمو "
			};
			var jobTitleAndDescriptions = new Dictionary<string, string>
			{
				{
					"مهندس برق", "مهندسی برق یکی از مشاغل مهم و کلیدی صنعت برق و مخابرات به شمار می رود."
				},
				{
					"مهندس معماری", "اگر از طراحی لذت برده و عاشق ساخت و ساز هم هستید، شغل مهندسی معماری می تواند برای شما ایده آل باشد."
				},
				{
					"مهندس شیمی", "مهندس شیمی فردی است که یافته های شیمیدان ها را به صورت عملی و کاربردی درآورده و در صنایع مختلف آنها را بکار می گیرد. در واقع مهندسی شیمی، فرآیند بکارگیری علم شیمی است."
				},
				{
					"مهندس سخت افزار", "مهندس سخت افزار کامپیوتر به تحقیق، طراحی، توسعه، تعمیر و آزمایش تجهیزات کامپیوتر مانند چیپ ها، صفحه مدارها، مونیتورها یا روترها می پردازد."
				},
				{
					"مهندس هوا فضا", "اگر شما عاشق هواپیما و فضاپیماها بوده و می خواهید برای پیشرفت و توسعه آنها کاری بکنید، شغل مهندسی هوافضا می تواند برای شما مناسب باشد."
				},
				{
					"مهندس دریا", "به دلیل داشتن مرزهای آبی گسترده و اهمیت بسیار زیاد حمل و نقل آبی در رشد و توسعه کشور، تخصص ها و مشاغل زیادی در حوزه دریایی ایجاد شده است. مهندسی دریا یکی از اصلی ترین آنها می باشد. مهندسی دریا چندین تخصص را در بر می گیرد که هر کدام حیطه وظایف مجزایی دارند."
				},
				{
					"مهندس کشاورزی", "مهندس کشاورزی فعالیت های مختلفی را انجام می دهد. او به توسعه استفاده از ماشین آلات کشاورزی و پیاده سازی فرآیندهای صحیح و علمی کشاورزی، باغبانی و جنگلداری می پردازد. مهندس کشاورزی باید دارای دانش و مهارت کافی برای کار کردن در صنایع مختلف کشاورزی و یا بخش بازرگانی و تجارت محصولات کشاورزی باشد."
				},
				{
					"مهندس بهداشت حرفه ای", "بهداشت حرفه‌ای یا سلامت شغلی یا سلامت کار شاخه‌ای است ازعلم بهداشت وعبارتست از شناسائی، ارزیابی و کنترل عوامل زیان آور موجود در محیط کار به همراه یکسری مراقبت های بهداشتی درمانی به منظور سالم سازی محیط کار و حفظ سلامت نیروی کار. بهداشت حرفه‌ای ترکیبی از علوم پزشکی ومهندسی می‌باشد."
				},
				{
					"مهندس عمران", "به عنوان مهندس عمران، شما به طراحی و مدیریت انواع پروژه های ساختمانی از تعمیر پل گرفته تا ساخت یک استادیوم ورزشی جدید می پردازید. برای مهندسی عمران باید در ریاضیات و مهارت های فناوری اطلاعات, عالی باشید. همچنین باید بتوانید به خوبی طرح ها و ایده های خود را برای دیگران بیان کرده و در نتیجه باید مهارت های ارتباطی بسیار خوبی داشته باشید. برای دست یابی به شغل مهندسی عمران باید مدارک معتبر دانشگاهی کسب نمایید."
				},
				{
					"مهندس صنایع", "در بازار رقابتی امروز، شرکت ها و سازمان ها برای بقا و رسیدن به اهداف خود باید از منابع موجود خود اعم از مالی و غیرمالی به صورت بهینه استفاده کنند تا هزینه های خود را به حداقل برسانند. علاوه بر این موارد تلاش در جهت افزایش کیفیت محصولات و خدمات در کنار بکارگیری نوآوری های مختلف، در موفقیت آنها بسیار موثر است. مهندسی صنایع ابزاری اساسی و موثر است که به مدیران و صاحبان شرکت ها و سازمان ها در انجام موارد فوق یاری می رساند."
				},
				{
					"مهندس نفت", "مهندس نفت روش های استخراج نفت و گاز از زیر زمین را طراحی کرده و توسعه می دهد.محل کار مهندس نفت در دفاتر کاری، پالایشگاه ها، محل های حفاری و یا آزمایشگاه های تحقیقاتی می باشد."
				},
				{
					"مهندس رباتیک", "مهندس رباتیک فردی است که پاسخگوی  نیاز صنعت در تحقیق و توسعه، طراحی، تولید، نگهداری و تعمیرات ربات ها می باشد."
				},
				{
					"مهندس معدن", "مهندسی معدن مجموعه علوم، روش ها و فنونی است که از اکتشاف یک معدن آغاز و تا فرآوری آن ادامه دارد. البته معدن باید از نظر اقتصادی ارزش و صرفه کافی را داشته باشد تا بتوان کار اکتشاف و استخراج (که فرآیندهایی پرهزینه هستند) را در آن انجام داد. از آنجا که از سالیان بسیار قبل، انسان ها به دنبال کشف طلا و برخی از مواد معدنی ارزشمند بوده اند، شغل مهندسی معدن دارای قدمت زیادی است."
				},
				{
					"مهندسی پزشکی", "مهندسی پزشکی کاربرد علوم مهندسی در حوزه پزشکی برای تشخیص و درمان بیماری ها است. حوزه مهندسی پزشکی به دنبال برطرف کردن نیازهای پزشکی در زمینه طراحی، ساخت و نگهداری تجهیزات و ابزارهای پزشکی برای کاربردهای پیشگیری، تشخیص و درمان بیماریها به کمک علوم مهندسی است."
				},
				{
					"مهندس بهداشت محیط", "مهندسی بهداشت محیط -بهداشت محیط کنترل همه عواملی است که اثر سویی بر پایدار ماندن سلامت انسان می‌گذارند. این شامل بیماری‌های زیادی می‌شود که از طریق آب، هوا، مواد غذایی و بسیاری از عوامل محیطی دیگر سلامت انسان را تهدید می‌کنند. برای رسیدن به این هدف، بهره‌گیری اصول مهندسی و دانش زیست‌محیطی به منظور کنترل، اصلاح و بهبود عوامل فیزیکی، شیمیایی و بیولوژیک محیط جهت حفظ و ارتقاء سلامتی و رفاه و آسایش انسان ضرورت می‌یابد."
				},
				{
					"مهندس راه آهن", "حمل و نقل یکی از عوامل اصلی و تعیین کننده در دنیای امروز است که در اقتصاد، فرهنگ و در همه شئون مهندس راه آهن – اجتماعی جوامع، نقش چشمگیر و حیاتی دارد. در میان انواع حمل ‌و نقل، حمل‌ و نقل ریلی از مزیت ‌های بسیاری برخوردار است و کشورهای صنعتی و نیمه‌ صنعتی از گذشته های دور به این پدیده پرارزش پرداخته و شبکه حمل و نقل خود را با حمل و نقل ریلی تجهیز کرده‌اند. تاجایی که امروزه خیلی از کشورها به مرحله‌ای رسیده‌اند که چندان به دنبال توسعه کمّی نیستند بلکه به دنبال هماهنگ کردن و هم سو کردن صنعت حمل و نقل ریلی با دیگر پدیده‌ های علمی و صنعتی پیشرو مانند الکترونیک و سیستم ‌های ارتباط جمعی می‌ باشند. در واقع هدف آن ها رسیدن سریع از مبدا به مقصد و امکان جابجایی پرحجم مسافر و کالا است."
				},
				{
					"مهندس مکانیک", "مهندس مکانیک، اصول اساسی نیرو، انرژی، حرکت و گرما را می‌آموزد و با دانش تخصصی خود، سیستم‌های مکانیکی و دستگاه‌ها و فرایندهای گرمایی را طراحی کرده و می‌سازد. مهندس مکانیک گستره وسیعی از دستگاه‌ها، فرآورده‌ها و فرایندها را تولید می‌کند؛ از قالب ساخت سوزن ته‌گرد تا مدل‌سازی حرکت ماهواره‌ها در فضای خارج از جو، موتورها و سیستم‌های کنترل خودرو و هواپیما، نیروگاه‌های الکتریکی، دستگاه‌های پزشکی و اجزا و قطعه‌های گوناگون و…."
				},
				{
					"مهندس مواد (مهندس متالورژی)", "مهندس مواد دست اندر کار استخراج، عمل آوری، و امتحان موادی است که در تولیدفراورده های گوناگون، از چیپهای کامپیوتری و صفحات تلوزیون گرفته تا فلز بکار رفته در خودروها به کار می روند. مهندس مواد با فلزات، سرامیک ها، مواد پلاستیکی، نیمه هادیها، و ترکیباتی از موادی که به آنها کامپوزیت (مواد مرکب) می‌گویند، برای بوجود آوردن موادی که دارای خصوصیات خاص مکانیکی، الکتریکی و شیمیائی باشند کار می کند. از جمله کارهای مهندس مواد انتخاب مواد برای کاربردهای جدید نیز میباشد."
				},
				{
					"مهندس پلیمر", "شما به عنوان مهندس پلیمر محصولاتی را طراحی، تولید و یا اصلاح می کنید که در همه جا وجود داشته و همه ما تا حد زیادی به آنها وابسته هستیم و شاید در دنیای امروز زندگی بدون مواد و محصولات پلیمری بسیار سخت باشد. نمونه ای ساده از این محصولات عبارتند از مسواک، تیوب خمیردندان، دمپایی پلاستیکی، بسیاری از لباس ها، ظروف پلاستیکی و ملامینی آشپزخانه، جلد برخی از کتاب ها، بسیاری از قطعات تلویزیون، کامپیوتر و …"
				}
			};

			#endregion

			#region MainGroups & SubGroups

			var seedMainGroups = new List<MainGroup>
			{
				new MainGroup
				{
					Title = "استخدام کشوری",
					ItemColor = ColorPallet.Goldenrod,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارمندان ثابت",
							ItemColor = ColorPallet.Goldenrod
						},
						new SubGroup
						{
							Title = "کارمندان رسمی",
							ItemColor = ColorPallet.Goldenrod
						},
						new SubGroup
						{
							Title = "شهردار",
							ItemColor = ColorPallet.Goldenrod
						}
					}
				},
				new MainGroup
				{
					Title = "بازنشستگان",
					ItemColor = ColorPallet.Teal,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "بازنشستگان",
							ItemColor = ColorPallet.Teal
						},
						new SubGroup
						{
							Title = "موظفین",
							ItemColor = ColorPallet.Teal
						}
					}
				},
				new MainGroup
				{
					Title = "تأمین اجتماعی",
					ItemColor = ColorPallet.CornflowerBlue,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارگران خدمات شهری",
							ItemColor = ColorPallet.CornflowerBlue
						},
						new SubGroup
						{
							Title = "کارگران خدمات اداری",
							ItemColor = ColorPallet.CornflowerBlue
						}
					}
				}
			};
			context.MainGroups.AddRange( seedMainGroups );

			#endregion

			#region Employees

			var employeeFaker = new Faker<Employee>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.FName = f.Name.FirstName();
				e.LName = f.Name.LastName();
				e.Address = $"{f.Address.City()} - {f.Address.CitySuffix()} - {f.Address.StreetName()} - پلاک  {f.Address.BuildingNumber()}";
				e.BirthPlace = f.Address.City();
				e.IdCardExportPlace = f.Address.City();
				e.CellNo = $"{f.PickRandom( mobilePrefix )}{f.Phone.PhoneNumber( "#######" )}";
				e.HomeTel = f.Phone.PhoneNumber();
				e.BirthDate = f.Date.Between( new PersianDate( 1330, 1, 1 ).ToDateTime(), new PersianDate( 1370, 12, 29 ).ToDateTime() );
				e.IdCardExportDate = f.Date.Between( new PersianDate( 1330, 1, 1 ).ToDateTime(), new PersianDate( 1370, 12, 29 ).ToDateTime() );
				e.DossierNo = $"{f.Random.Number( 999999 ):d6}";
				e.PersonnelCode = $"{f.Random.Number( 999999 ):d6}";
				e.FatherName = f.Name.FirstName();
				e.Sex = f.PickRandom( Enum.GetValues( typeof(Sex) ).Cast<Sex>().Where( sex => sex != Sex.Unknown ) );
				e.PostalCode = $"{f.Random.Number( 999999999 ):d10}";
				e.NationalCardNo = $"{f.Random.Number( 999999999 ):d10}";
				e.IdCardNo = $"{f.Random.Number( 999999 )}";
			} );
			var seedEmployees = employeeFaker.Generate( 200 );
			context.Employees.AddRange( seedEmployees );

			#endregion

			#region ExpenseArticles

			var seedExpenseArticles = new List<ExpenseArticle>();
			var expenseArticleFaker = new Faker<ExpenseArticle>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Title = f.PickRandom( expenseArticles.Where( s => !seedExpenseArticles.Select( x => x.Title ).Contains( s ) ) );
				e.Code = $"{f.Random.Number( 99999 ):D5}";
				e.IsActive = true;
			} );
			for( var i = 0; i < 10; i++ )
				seedExpenseArticles.Add( expenseArticleFaker.Generate() );

			seedExpenseArticles.AddRange( seedExpenseArticles );

			#endregion

			#region Cities

			var seedCities = new List<City>();
			var CityFaker = new Faker<City>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Title = f.PickRandom( cityNames.Where( s => !seedCities.Select( city => city.Title ).Contains( s ) ) );
				e.Distance = f.Random.Int( 20, 999 );
				e.Percentage = f.Random.Number( 100 );
			} );
			for( var i = 0; i < 50; i++ )
				seedCities.Add( CityFaker.Generate() );

			context.Cities.AddRange( seedCities );

			#endregion

			#region Jobs

			var seedJobs = new List<Job>();
			var jobFaker = new Faker<Job>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.JobNo = $"{f.Random.Number( 999999 ):d6}";
				e.ItemColor = f.PickRandom( Enum.GetValues( typeof(ColorPallet) ).Cast<ColorPallet>().Where( x => x != ColorPallet.Unknown ) );
			} );
			foreach( var job in jobTitleAndDescriptions )
			{
				jobFaker.RuleFor( j => j.Title, job.Key ).RuleFor( j => j.Description, job.Value );
				seedJobs.Add( jobFaker.Generate() );
			}

			context.Jobs.AddRange( seedJobs );

			#endregion

			#region ContractFields

			var seedContractFields = new List<ContractField>();
			var contractFieldTitleFaker = new Faker<ContractField>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.SubGroup = f.PickRandom( f.PickRandom( seedMainGroups ).SubGroups );
				e.Year = 1397;
			} );
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 0; i < 10; i++ )
					{
						contractFieldTitleFaker.RuleFor( field => field.Title, _faker.PickRandom( contractFields ) );
						var newContractField = contractFieldTitleFaker.Generate();
						if( !seedContractFields.Where( field => field.SubGroup.Equals( subGroup ) ).Select( field => field.Title ).Contains( newContractField.Title ) )
							seedContractFields.Add( newContractField );
					}

			context.ContractFields.AddRange( seedContractFields );

			#endregion

			#region MiscTitles

			var seedMiscTitles = new List<MiscTitle>();
			foreach( var title in miscTitlesPayment )
				seedMiscTitles.Add( new MiscTitle
				{
					Title = title,
					IsPayment = true
				} );
			foreach( var title in miscTitlesDebt )
				seedMiscTitles.Add( new MiscTitle
				{
					Title = title,
					IsPayment = false
				} );

			context.MiscTitles.AddRange( seedMiscTitles );

			#endregion

			#region Miscs

			var miscFaker = new Faker<Misc>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Year = 1397;
				e.MiscTitle = f.PickRandom( seedMiscTitles );
			} );
			var seedMiscs = new List<Misc>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 0; i < 10; i++ )
					{
						miscFaker.RuleFor( parameter => parameter.SubGroup, subGroup );
						seedMiscs.Add( miscFaker.Generate() );
					}

			context.Miscs.AddRange( seedMiscs );

			#endregion

			#region TaxTables

			var TaxTableFaker = new Faker<TaxTable>( "fa" ).StrictMode( false ).Rules( ( f, e ) => { e.Month = 007; } );
			var seedTaxTables = new List<TaxTable>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 1395; i <= 1397; i++ )
					{
						TaxTableFaker.RuleFor( TaxTable => TaxTable.SubGroup, subGroup ).RuleFor( TaxTable => TaxTable.Year, i );
						seedTaxTables.Add( TaxTableFaker.Generate() );
					}

			context.TaxTables.AddRange( seedTaxTables );
			var TaxRowFaker = new Faker<TaxRow>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.ValueTo = f.Random.Number( 20000 ) * 1000;
				e.Factor = f.Random.Number( 10 );
			} );
			var seedTaxRows = new List<TaxRow>();
			foreach( var taxTable in seedTaxTables )
				for( var i = 0; i < 10; i++ )
				{
					TaxRowFaker.RuleFor( row => row.TaxTable, taxTable );
					seedTaxRows.Add( TaxRowFaker.Generate() );
				}

			context.TaxRows.AddRange( seedTaxRows );

			#endregion

			#region MissionFormulas

			var MissionFormulaFaker = new Faker<MissionFormula>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Month = 007;
				e.DivideFactor = f.Random.Number( 30 );
				e.AddFactor = f.Random.Number( 250000 );
				e.MaxFactor = f.Random.Number( 1000000 );
				e.PerKmFactor = f.Random.Number( 500 );
				e.MissionFormulaInvolvedContractFields = new List<MissionFormulaInvolvedContractField>();
			} );
			var seedMissionFormulas = new List<MissionFormula>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 1395; i <= 1397; i++ )
					{
						MissionFormulaFaker.RuleFor( missionFormula => missionFormula.SubGroup, subGroup ).RuleFor( missionFormula => missionFormula.Year, i );
						seedMissionFormulas.Add( MissionFormulaFaker.Generate() );
					}
			foreach( var missionFormula in seedMissionFormulas )
				for( var i = 0; i < 10; i++ )
					if( _faker.Random.Bool() )
					{
						var item = new MissionFormulaInvolvedContractField();
						var validContractFields = missionFormula.SubGroup.ContractFields.Where( field => !missionFormula.MissionFormulaInvolvedContractFields.Select( invFld => invFld.ContractField ).Contains( field ) );
						if( validContractFields.Any() )
						{
							item.ContractField = _faker.PickRandom( validContractFields );
							item.MissionFormula = missionFormula;
							missionFormula.MissionFormulaInvolvedContractFields.Add( item );
						}
					}

			context.MissionFormulas.AddRange( seedMissionFormulas );

			#endregion

			#region HandselFormulaFaker

			var HandselFormulaFaker = new Faker<HandselFormula>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Month = 007;
				e.DaysCount = f.Random.Number( 60 );
				e.TaxRate = f.Random.Number( 10 );
				e.Max = f.Random.Number( 5000 ) * 1000;
				e.Min = f.Random.Number( 5000 ) * 1000;
				e.TaxFreeValue = f.Random.Number( 5000 ) * 1000;
			} );
			var seedHandselFormulas = new List<HandselFormula>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 1395; i <= 1397; i++ )
					{
						HandselFormulaFaker.RuleFor( HandselFormula => HandselFormula.SubGroup, subGroup ).RuleFor( HandselFormula => HandselFormula.Year, i );
						seedHandselFormulas.Add( HandselFormulaFaker.Generate() );
					}

			context.HandselFormulas.AddRange( seedHandselFormulas );

			#endregion

			#region Parameters

			var ParameterFaker = new Faker<Parameter>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Year = 1397;
				e.Month = 007;
				e.Alias = f.Finance.AccountName().Replace( " ", string.Empty );
				e.Title = f.PickRandom( parameterTitles );
				e.ValueType = f.PickRandom( Enum.GetValues( typeof(ValueType) ).Cast<ValueType>().Where( valueType => valueType != ValueType.Unknown ) );
				e.Value = f.Random.Number( 999999 );
				e.ParameterInvolvedContractFields = new List<ParameterInvolvedContractField>();
				e.ParameterInvolvedMiscs = new List<ParameterInvolvedMisc>();
			} );
			var seedParameters = new List<Parameter>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					for( var i = 0; i < 10; i++ )
					{
						ParameterFaker.RuleFor( parameter => parameter.SubGroup, subGroup );
						seedParameters.Add( ParameterFaker.Generate() );
					}

			#region ParameterInvolvedContractFields

			foreach( var parameter in seedParameters )
				foreach( var contractField in parameter.SubGroup.ContractFields )
					if( _faker.Random.Bool() && !parameter.ParameterInvolvedContractFields.Select( inv => inv.ContractField ).Contains( contractField ) )
						parameter.ParameterInvolvedContractFields.Add( new ParameterInvolvedContractField
						{
							ContractField = contractField
						} );

			#endregion

			#region ParameterInvolvedMiscs

			foreach( var parameter in seedParameters )
				foreach( var misc in parameter.SubGroup.Miscs )
					if( _faker.Random.Bool() && !parameter.ParameterInvolvedMiscs.Select( inv => inv.Misc ).Contains( misc ) )
						parameter.ParameterInvolvedMiscs.Add( new ParameterInvolvedMisc
						{
							Misc = misc
						} );

			#endregion

			context.Parameters.AddRange( seedParameters );

			#endregion

			#region ExpenseArticleOfMiscForSubGroups

			var seedExpenseArticleOfMiscForSubGroups = new List<ExpenseArticleOfMiscForSubGroup>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					foreach( var misc in subGroup.Miscs )
						seedExpenseArticleOfMiscForSubGroups.Add( new ExpenseArticleOfMiscForSubGroup
						{
							SubGroup = subGroup,
							Month = 007,
							Misc = misc,
							ExpenseArticle = _faker.PickRandom( seedExpenseArticles )
						} );

			context.ExpenseArticleOfMiscForSubGroups.AddRange( seedExpenseArticleOfMiscForSubGroups );

			#endregion

			#region ExpenseArticleOfContractFieldForSubGroups

			var seedExpenseArticleOfContractFieldForSubGroups = new List<ExpenseArticleOfContractFieldForSubGroup>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					foreach( var contractField in subGroup.ContractFields )
						seedExpenseArticleOfContractFieldForSubGroups.Add( new ExpenseArticleOfContractFieldForSubGroup
						{
							SubGroup = subGroup,
							Month = 007,
							ContractField = contractField,
							ExpenseArticle = _faker.PickRandom( seedExpenseArticles )
						} );

			context.ExpenseArticleOfContractFieldForSubGroups.AddRange( seedExpenseArticleOfContractFieldForSubGroups );

			#endregion

			#region ExpenseArticleOfOverTimeForSubGroups

			var seedExpenseArticleOfOverTimeForSubGroups = new List<ExpenseArticleOfOverTimeForSubGroup>();
			foreach( var mainGroup in seedMainGroups )
				foreach( var subGroup in mainGroup.SubGroups )
					seedExpenseArticleOfOverTimeForSubGroups.Add( new ExpenseArticleOfOverTimeForSubGroup
					{
						SubGroup = subGroup,
						Year = 1397,
						Month = 007,
						ExpenseArticle = _faker.PickRandom( seedExpenseArticles )
					} );

			context.ExpenseArticleOfOverTimeForSubGroups.AddRange( seedExpenseArticleOfOverTimeForSubGroups );

			#endregion

			#region ContractMaster

			var contractMasterFaker = new Faker<ContractMaster>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.Employee = f.PickRandom( seedEmployees );
				e.AccountNoEmp = $"{f.PickRandom( creditCardNoPrefix )}-{f.Random.Number( 9999 ):d4}-{f.Random.Number( 9999 ):d4}-{f.Random.Number( 9999 ):d4}";
				e.AccountNoGov = $"{f.PickRandom( creditCardNoPrefix )}-{f.Random.Number( 9999 ):d4}-{f.Random.Number( 9999 ):d4}-{f.Random.Number( 9999 ):d4}";
				e.ContractNo = $"{f.Random.Number( 99999 ):d5}";
				e.DateEmployment = f.Date.Between( e.Employee.BirthDate.AddYears( 20 ), DateTime.Now );
				e.DateExport = f.Date.Between( e.DateEmployment, DateTime.Now );
				e.DateExecution = f.Date.Between( e.DateExport, DateTime.Now );
				e.EducationStand = f.PickRandom( Enum.GetValues( typeof(EducationStand) ).Cast<EducationStand>().Where( sex => sex != EducationStand.Unknown ) );
				e.EmploymentType = f.PickRandom( Enum.GetValues( typeof(EmploymentType) ).Cast<EmploymentType>().Where( sex => sex != EmploymentType.Unknown ) );
				e.HardshipFactor = f.Random.Number( 100 );
				e.InsuranceNo = $"{f.Random.Number( 99999999 ):d8}";
				e.MaritalStatus = f.PickRandom( Enum.GetValues( typeof(MaritalStatus) ).Cast<MaritalStatus>().Where( sex => sex != MaritalStatus.Unknown ) );
				e.Job = f.PickRandom( seedJobs );
				e.SacrificeStand = f.PickRandom( Enum.GetValues( typeof(SacrificeStand) ).Cast<SacrificeStand>().Where( sex => sex != SacrificeStand.Unknown ) );
				e.SubGroup = f.PickRandom( f.PickRandom( seedMainGroups ).SubGroups );
			} );
			var seedContractMasters = contractMasterFaker.Generate( 200 );
			context.ContractMasters.AddRange( seedContractMasters );

			#endregion

			#region ContractDetail

			var contractDetailFaker = new Faker<ContractDetail>( "fa" ).StrictMode( false ).Rules( ( f, e ) => { e.Value = f.Random.Number( 100 ) * 10000; } );
			var seedContractDetails = new List<ContractDetail>();
			foreach( var contMast in seedContractMasters )
				foreach( var grpCntField in seedContractFields.Where( c => c.SubGroup.Equals( contMast.SubGroup ) ) )
				{
					contractDetailFaker.RuleFor( detail => detail.ContractMaster, contMast );
					contractDetailFaker.RuleFor( detail => detail.ContractField, grpCntField );
					var contractDetail = contractDetailFaker.Generate();
					seedContractDetails.Add( contractDetail );
				}

			context.ContractDetails.AddRange( seedContractDetails );

			#endregion

			#region Set CurrentContract for each Employee

			var queryEmpContracts = from cont in seedContractMasters
			                        group cont by cont.Employee
			                        into newGroup
			                        select newGroup;
			foreach( var empContract in queryEmpContracts )
			{
				var lastCnt = empContract.LastOrDefault();
				if( lastCnt != null )
					lastCnt.IsCurrent = true;
			}

			#endregion

			#region MiscRecharges

			var seedMiscRecharges = new List<MiscRecharge>();
			var miscRechargeFaker = new Faker<MiscRecharge>( "fa" ).StrictMode( false ).Rules( ( f, e ) => { e.Value = f.Random.Number( 100 ) * 1000; } );
			var firstTenContracts = seedContractMasters.Where( master => master.IsCurrent ).Take( 10 );
			foreach( var contract in firstTenContracts )
			{
				var contractMiscs = contract.SubGroup.Miscs.Take( _faker.Random.Number( contract.SubGroup.Miscs.Count ) );
				foreach( var misc in contractMiscs )
				{
					miscRechargeFaker.RuleFor( r => r.Employee, contract.Employee ).RuleFor( r => r.Misc, misc );
					for( var y = 1395; y <= 1397; y++ )
						for( var m = 5; m <= 11; m++ )
						{
							miscRechargeFaker.RuleFor( r => r.Year, y ).RuleFor( r => r.Month, m );
							seedMiscRecharges.Add( miscRechargeFaker );
						}
				}
			}

			context.MiscRecharges.AddRange( seedMiscRecharges );

			#endregion

			#region Missions

			var seedMissions = new List<Mission>();
			var MissionFaker = new Faker<Mission>( "fa" ).StrictMode( false ).Rules( ( f, e ) =>
			{
				e.AmountNonResident = f.Random.Number( 15 );
				e.AmountResident = f.Random.Number( 15 );
				e.City = f.PickRandom( seedCities );
				e.MissionContractNo = f.Random.Number( 99999 ).ToString( "d5" );
				e.VehicleType = f.PickRandom( Enum.GetValues( typeof(VehicleType) ).Cast<VehicleType>().Where( v => v != VehicleType.Unknown ) );
				e.TravelExpense = f.Random.Number( 1000 ) * 1000;
			} );

			foreach( var contract in seedContractMasters )
			{
				var startMonth = _faker.Random.Number( 9 ) + 1;
				MissionFaker.RuleFor( m => m.ContractMaster, contract );
				MissionFaker.RuleFor( m => m.StartDate, _faker.Date.Between( contract.DateEmployment, DateTime.Now ) );
				MissionFaker.RuleFor( m => m.EndDate, ( f, m ) => f.Date.Between( m.StartDate, DateTime.Now ) );
				MissionFaker.RuleFor( m => m.StartTime, _faker.Date.Past(  ) );
				MissionFaker.RuleFor( m => m.EndTime, _faker.Date.Past(  ) );

				for( var i = 0; i < 10; i++ )
					seedMissions.Add( MissionFaker.Generate() );
			}

			context.Missions.AddRange( seedMissions );

			#endregion

			base.Seed( context );
		}
	}
}