namespace Business.Constants
{
    public static class Messages
    {
         public static string OperationClaimExists => "Bu Operasyon izni zaten mevcut";
        public static string AuthorizationsDenied = "Yetkiniz Olmayan Bir Alana Giriş Yapmaya Çalıştığınız Tespit Edildi!";

        public static string Added = "Başarıyla eklendi.";
        public static string Deleted = "Başarıyla silindi.";
        public static string Updated = "Başarıyla güncellendi.";

        public static string UserNotFound = "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını Kullanınız.";
        public static string UserNotApprovedProfile = "Profiliniz onaylanmamış Sistem Yöneticisi ile iletişime geçiniz.";
        public static string PasswordError = "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı.";
        public static string SuccessfulLogin = "Sisteme giriş başarılı.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string UserClaimsNotFound = "Bu kullanıcı zaten mevcut.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserCantAdded = "Kullanıcı Kayıt Hatası..";
        public static string SendMobileCode = "Lütfen Size SMS Olarak Gönderilen Kodu Girin!";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";

        public static string NameAlreadyExist = "Oluşturmaya Çalıştığınız Nesne Zaten Var.";

        public static string StartsWithA = "Ürün ismi Büyük A harfiyle başlamalı";
        public static string CIDAlreadyExist = "T.C.Kimlik No Sistemimizde Kayıtlı Lütfen Sisteme Giriş Yapınız.";
        public static string WrongCID = "T.C.Kimlik No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!";

        public static string PasswordEmpty = "Parola Boş Olamaz!";
        public static string PasswordLength = "Minimum 8 Karakter Uzunluğunda Olmalıdır!";
        public static string PasswordUppercaseLetter = "En Az 1 Büyük Harf İçermeledir!";
        public static string PasswordLowercaseLetter = "En Az 1 Küçük Harf İçermeledir!";
        public static string PasswordDigit = "En Az 1 Rakam İçermeledir!";
        public static string PasswordSpecialCharacter = "En Az 1 Simge İçermelidir!";
        public static string SendPassword = "Yeni Parolanız E-Posta Adresinize Gönderildi.";
        public static string LimitedRecord = "Bu Kayıtta Değişiklik Yapabilmek Yeterli Süre Geçmemiş! 6 Ayda bir Kayıt Değiştirme İzni Verilmiştir.";
        public static string NoNeedForNeedCode = "Daha Önce Aldığınız Kod 24 Saat Geçerli.";
        public static string InvalidCode = "Geçersiz Bir Kod Girdiniz!";
        public static string SmsServiceNotFound = "SMS Servisine Ulaşılamıyor.";
        public static string IsCidValid = "Geçerli Bir T.C.Kimlik Numarası Giriniz!";
        public static string SmsServiceNotFoundForAppointment = "Randevu Oluşturuldu Ancak SMS Servisine Ulaşılamıyor!";
        public static string CouldNotBeVerifyCid = "TC Kimlik Sistemi Tarafından Doğrulanamadı.";
        public static string VerifyCid = "TC Kimlik Sistemi Tarafından Doğrulandı.";
        public static string ProductUpdated = "Ürün Güncellendi";
        public static string ProductDeleted = "Ürün Silindi";
        public static string ProductNotFound="Ürün bulunamadı...";   
        public static string ProductAdded = "Ürün Eklendi";
        public static string SaveProblem ="Yeni kayıt eklemenemedi...";
        public static string UpdateProblem ="Yeni kayıt Güncellenemedi...";
        public static string DeleteProblem ="Kayıt Silinemedi...";
        public static string TokenNotCreated="Access token oluşturulamadı..";
        internal static string ProductNotAdded="Ürün Eklenemedi..";

        public static string UserExists => "Bu İsimde Kullanıcı mevcut.";
        public static string WrongPassword => "Şifre Hatalı";
        public static string AuthorizationDenied => "Erişim için yetkiniz yok.";

        public static string GroupClaimAdded => "Grup izni başarıyla eklendi.";
        public static string GroupClaimUpdated => "Grup izni başarıyla güncellendi.";
        public static string GroupClaimDeleted => "Grup izni başarıyla silindi.";

        public static string GroupDeleted => "Grup başarıyla silindi.";
        public static string GroupUpdated => "Grup başarıyla güncellendi.";
        public static string GroupAdded => "Grup başarıyla eklendi.";

        public static string OperationClaimAdded => "Operasyon izni başarıyla eklendi.";
        public static string OperationClaimUpdated => "Operasyon izni başarıyla güncellendi.";
        public static string OperationClaimDeleted => "Operasyon izni başarıyla silindi.";

        public static string UserClaimCreated => "Kullanıcı izni oluşturuldu.";
        public static string UserClaimUpdated => "Kullanıcı izni güncellendi.";
        public static string UserClaimDeleted => "Kullanıcı izni silindi.";

        public static string UserGroupAdded => "Kullanıcı gruba eklendi.";
        public static string UserGroupUpdated => "Kullanıcı grubu güncellendi.";
        public static string UserGroupDeleted => "Kullanıcı gruptan çıkarıldı.";

        public static string CategoryNotFound=>"Kategori bulunamadı...";
        public static string CampusListNotFound=>"Yerleşke Listesi alınamadı veya yok...";
        public static string DepartmentListNotFound=>"Birim Listesi alınamadı veya yok...";
        public static string TitleListNotFound=>"Ünvan Listesi alınamadı veya yok...";
        public static string RoleListNotFound=>"Role Listesi alınamadı veya yok...";
        public static string RoleCatgegorListListNotFound=>"Role Listesi alınamadı veya yok...";
        public static string RoleNameAlreadyExist=>"Bu yetki ismi zaten kayıtlı...";
        public static string AlreadyExist=>"Id veya bu isimle kayıt zaten var...";
        public static string NotFound=>"Kayıt bulunamadı...";
        public static string BrandNotFound=>"Araç Markası bulunamadı...";
        public static string HomeAnnounceEmpty=>"Ev ilanları listelenemedi...";
        public static string SubScreenAlreadyExist=>"Bu İlan/Yayın için bu yayın ekranı zaten seçilmiş ...";
        public static string NotFoundSubSCreen=>"Yayın Ekranı bulunamadı ...";
        public static string NotFoundScreen=>"Ana Ekran bulunamadı ...";
        public static string NotFoundAnnounce=>"İlan/Yayın bulunamadı ...";
        public static string NotSelectSubScreen=>"İlan/Yayın'ı yayınlamak için yayın ekranı  seçmelisiniz...";
        public static string PublishDateExpire=>"Yayın tarihlerinin süresi geçmiş yayın tarihlerini güncelleyin.";
        


    }
}