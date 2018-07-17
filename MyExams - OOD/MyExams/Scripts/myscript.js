


// Slow scrolling when clicking links 
        $("a").on('click', function(event) {
            if (this.hash !== "") {
                event.preventDefault();
                var hash = this.hash;
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function() {
                    window.location.hash = hash;
                });
            }
});

// When the user scrolls down, #myBtn shows
window.onscroll = function () {
    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        $("#goToTopBtn").show();
    } else {
        $("#goToTopBtn").hide();
    }
};

// When the user clicks on the button, scroll to the top of the document
$('#goToTopBtn').each(function () {
    $(this).click(function () {
        $('html,body').animate({
            scrollTop: 0
        }, 'slow');
        return false;
    });
});


// animated counter 

$('.percentage').each(function () {
    var $this = $(this);
    jQuery({ Counter: 0 }).animate({ Counter: $this.text() }, {
        duration: 1000,

        step: function () {

            if ($this.text().indexOf(".") != -1) {
            $this.text(this.Counter.toFixed(2));

            } else {
                $this.text(Math.ceil(this.Counter));

            }
        }
    });
});




$('.sortable').click(function () {
    var table = $(this).parents('table').eq(0);
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()));
    this.asc = !this.asc;
    if (!this.asc) { rows = rows.reverse(); }
    for (var i = 0; i < rows.length; i++) { table.append(rows[i]); }
});
function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index), valB = getCellValue(b, index);
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
    };
}
function getCellValue(row, index) { return $(row).children('td').eq(index).text(); }


//search function
$("#searchTeacher").on('keyup', function () {
    var searchedItem, teacher, td, i;
    searchedItem = this.value.toUpperCase();
    teacher = $(".teachers-div")

    for (i = 0; i < teacher.length; i++) {
        td = $(teacher[i]).children("h3")[0];
        if (td != null) {
            if (td.innerHTML.toUpperCase().indexOf(searchedItem) > -1) {
                $(teacher[i]).fadeIn(1000);

            } else {
                $(teacher[i]).fadeOut(300);

            }
        }
    }
});
