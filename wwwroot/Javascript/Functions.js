
  var userRating = 0;

    function rateProduct(rating) {
        userRating = rating;
        highlightStars(rating);
    }

    function highlightStars(count) {
        var stars = document.querySelectorAll('.rating .star');

        stars.forEach(function (star, index) {
            if (index < count) {
                star.classList.add('selected');
            } else {
                star.classList.remove('selected');
            }
        });
    }

  function submitReview() {
    if (userRating > 0) {
        alert('تم استلام تقييمك: ' + userRating);
        // يمكنك إرسال التقييم إلى الخادم أو قاعدة البيانات هنا
    } else {
        alert('يرجى تحديد تقييم');
    }

    if (userRating > 0) {
        alert(translations[selectedLanguage]['submitMessage'] + ': ' + userRating);
        // يمكنك إرسال التقييم إلى الخادم أو قاعدة البيانات هنا
    } else {
        alert(translations[selectedLanguage]['selectRatingMessage']);
        // يرجى تحديد تقييم
    }
}
function rateProduct(rating) {
    userRating = rating;
    highlightStars(rating);

    // أضف تحديثًا لتغيير لون النجوم المحددة إلى الذهبي
    var stars = document.querySelectorAll('.rating .star');
    stars.forEach(function (star, index) {
        if (index < rating) {
            star.classList.add('selected');
        } else {
            star.classList.remove('selected');
        }
    });
}
function showContent(tabName) {
    var tabContents = document.getElementsByClassName('tab-content');
    for (var i = 0; i < tabContents.length; i++) {
        tabContents[i].style.display = 'none';
    }

    var categoryElement = document.getElementById(tabName);
    if (categoryElement) {
        categoryElement.style.display = 'grid';
    }
}
function logout() {
    // حذف بيانات الدخول من Local Storage
    localStorage.removeItem('username');
    localStorage.removeItem('password');
    // إخفاء رابط تسجيل الخروج
    document.getElementById('logoutLink').style.display = 'none';
    alert('تم تسجيل الخروج بنجاح!');
    // توجيه المستخدم إلى الصفحة الرئيسية أو أي صفحة أخرى
    window.location.href = "front_home";
}

function deleteSection(sectionId) {
    // العثور على القسم المحدد
    var section = document.getElementById(sectionId);
    // العثور على جميع حقول الإدخال والاختيار داخل القسم
    var elements = section.querySelectorAll('input, select');
    // إفراغ قيمة كل عنصر
    elements.forEach(function(element) {
        if (element.tagName === 'INPUT') {
            // إذا كان العنصر هو input
            if (element.type === 'text' || element.type === 'password' || element.type === 'email' || element.type === 'tel' || element.type === 'number' || element.type === 'date') {
                // إفراغ حقول الإدخال من النوع text أو password أو email أو tel أو number أو date
                element.value = '';
            } else if (element.type === 'checkbox') {
                // إلغاء تحديد حقول الاختيار من النوع checkbox
                element.checked = false;
            } else if (element.type === 'radio') {
                // إلغاء تحديد جميع الخيارات لحقول الاختيار من النوع radio buttons
                element.checked = false;
            }
        } else if (element.tagName === 'SELECT') {
            // إفراغ قيمة حقل select
            element.selectedIndex = 0; // يتم تحديد الخيار الأول بشكل افتراضي
        }
    });
}
// استدعاء السائق لتحديث حالة الطلب
function updateOrderStatus(orderId, newStatus) {
    // يجب تحديث قاعدة البيانات أو إرسال طلب للخادم لتحديث حالة الطلبية

    // مثال بسيط: تحديث حالة الطلبية مباشرة في الصفحة
    const eventElement = document.querySelector(`#timeline [data-order="${orderId}"]`);
    if (eventElement) {
        // تحديث حالة الطلبية بناءً على القيمة الجديدة
        eventElement.setAttribute('data-status', newStatus);
        // تحديث نص الحدث بناءً على الحالة الجديدة
        switch (newStatus) {
            case 'loading':
                eventElement.querySelector('span').innerText = 'تحميل البضائع';
                break;
            case 'shipped':
                eventElement.querySelector('span').innerText = 'شحن البضائع';
                break;
            case 'delivered':
                eventElement.querySelector('span').innerText = 'توصيل البضائع';
                break;
            default:
                break;
        }
    }
}

// مثال على كيفية استدعاء الوظيفة لتحديث حالة الطلب
updateOrderStatus('123', 'shipped'); // تحديث حالة الطلبية للطلب رقم 123 إلى "تم شحنه"


function showSection(sectionId) {
    // إزالة الفئة "active-section" من جميع الأقسام
    var sections = document.querySelectorAll('.section');
    sections.forEach(function(section) {
        section.classList.remove('active-section');
    });

    // إضافة الفئة "active-section" للقسم المحدد
    var selectedSection = document.getElementById(sectionId);
    selectedSection.classList.add('active-section');

    // إظهار زر الحفظ بشكل افتراضي
    var saveIcon = selectedSection.querySelector('.save-icon');
    if (saveIcon) {
        saveIcon.style.display = 'inline-block';
    }
}

function redirectToHomePage() {
    window.location.href = "front_home";
}
// سكريبت لتحرير وحفظ المعلومات الشخصية
function editSection(sectionId) {
    const section = document.getElementById(sectionId);
    const inputs = section.querySelectorAll('input, select');
    inputs.forEach(input => {
        input.removeAttribute('disabled'); // تمكين جميع حقول الإدخال للتعديل
    });

    const saveIcon = section.querySelector('.save-icon');
    saveIcon.style.display = 'inline-block'; // عرض أيقونة الحفظ
}

function saveChanges(sectionId) {
    const section = document.getElementById(sectionId);
    const inputs = section.querySelectorAll('input, select');
    inputs.forEach(input => {
        input.setAttribute('disabled', 'disabled'); // تعطيل جميع حقول الإدخال بعد الحفظ
    });

    const saveIcon = section.querySelector('.save-icon');
    saveIcon.style.display = 'none'; // إخفاء أيقونة الحفظ

    // إرسال طلب لحفظ التغييرات، مثلاً إرسال طلب AJAX لتحديث المعلومات الشخصية في قاعدة البيانات
}


