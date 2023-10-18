

Dropzone.autoDiscover = false;
var dz;
var objAsset;

$(document).ready(function () {
    fnBindFiles();
    fnCreateDropzone();
});

$(function () {
    // Multiple images preview in browser
    function imagesPreview(input, placeToInsertImagePreview) {

        if (input.files) {
            var filesAmount = input.files.length;
            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var $Gal_preview = '<div class="gal_box"><img src=' + event.target.result + ' /></div>';
                    $($Gal_preview).appendTo(placeToInsertImagePreview);
                }
                reader.readAsDataURL(input.files[i]);
            }
        }
    };

    $('#fupGalleryImages').on('change', function () {
        $("#uploadGalleryPreview").html('');
        imagesPreview(this, '#uploadGalleryPreview');

    });
});

function editGallery(ID, src, contentType) {
    clearAll();
    if (contentType == 'audio/video') {
        $("#videoGallery").attr('src', 'https://storage.googleapis.com/navtejcms/post/' + src);
        $("#imgGallery").hide();
        $("#videoGallery").show();
    }
    else {
        $("#imgGallery").attr('src', 'https://storage.googleapis.com/navtejcms/post/' + src);
        $("#videoGallery").hide();
        $("#imgGallery").show();
    }
    $("#url").val('https://storage.googleapis.com/navtejcms/post/' + src);
    $("#hdnGallery").val(ID);
    $("#hdnImageURL").val('https://storage.googleapis.com/navtejcms/post/' + src);
    $("#hdnImageSRC").val(src);
    $("#hdnContentType").val(contentType);
    $("#rightSideBox").show();
}

function copy() {
    // Get the text field
    var copyText = document.getElementById("url");

    // Select the text field
    copyText.select();
    copyText.setSelectionRange(0, 99999); // For mobile devices

    // Copy the text inside the text field
    navigator.clipboard.writeText(copyText.value);

    // Alert the copied text
    $("#copyAlert").html("Copied !");
}
function clearAll() {
    $("#url").val("");
    $("#hdnGallery").val('');
    $("#copyAlert").html("");
}

function fnCreateDropzone() {

    var employeeID = $("#hdnEmployeeID").val();
    var url1 = $("#hidWebPath").val() + "/Admin/uploadHandler.ashx?employeeID=" + employeeID;
    if (dz != null) {
        return;
    }

    dz = $("#dZUpload").dropzone({
        url: url1,
        autoProcessQueue: true,
        parallelUploads: 6,
        uploadMultiple: true,
        //File size
        maxfilesize: 20,
        //Number of files
        maxFiles: 6,
        addRemoveLinks: true,
        init: function () {
            this.on("processing", function (file) {
                url1 = $("#hidWebPath").val() + "/Admin/uploadHandler.ashx?employeeID=" + employeeID;
                this.options.url = url1;
            });
        },
        success: function (file, response) {
            var res = response;
            file.previewElement.classList.add("dz-success");
            console.log("Successfully uploaded :" + res);
            var pattern = '20 MB';
            if (res.indexOf(pattern) != -1) {
                alert('File size exceed 20 MB');
            }
        },
        error: function (file, response) {
            file.previewElement.classList.add("dz-error");
        },
        complete: function (file) {
            file.previewElement.remove();
            $("#dZUpload").removeClass("dz-started");
            fnBindFiles();
        }
    });
}

function fnBindFiles() {
    var url = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=getassetsforgalleryonly";
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: url,
        success: function (response) {
            if (response != "" && response != 'undefined') {
                objAsset = JSON.parse(response);
                console.log(objAsset);

                var allItems = "";
                var allVideoItems = "";
                for (var i = 0; i < objAsset.length; i++) {
                    var id = objAsset[i].ID;
                    var contentType = objAsset[i].ContentType;
                    var contentTypeStr = '\'' + objAsset[i].ContentType + '\'';
                    var uniqueNameID = '\'' + objAsset[i].UniqueName + '\'';
                    var uniqueName = objAsset[i].UniqueName;
                    if (contentType == 'audio/video') {
                        var videoStr = '<video class="VideoClass" src="https://storage.googleapis.com/navtejcms/post/' + uniqueName + '" controls></video>';
                        var videoItem = '<div class="borderBoxVideo"><div class="attachment"><a onclick="editGallery(' + id + ', ' + uniqueNameID + ', ' + contentTypeStr + ')">' + videoStr + '</a></div></div>';
                        allVideoItems += videoItem;
                    }
                    else {
                        var item = '<div class="borderBox"><div class="attachment"><a onclick="editGallery(' + id + ', ' + uniqueNameID + ', ' + contentTypeStr + ')"><img src="https://storage.googleapis.com/navtejcms/post/' + uniqueName + '" class="imgPost" /></a></div></div>';
                        allItems += item;
                    }

                }

                $("#galleryItems").html(allItems);
                $("#galleryVideoItems").html(allVideoItems);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
    $("#rightSideBox").hide();
}

function confirmDelete() {
    if (confirm('Are you sure?')) {
        fnDeleteAsset();
    }
}

function fnDeleteAsset() {
    var ID = $("#hdnGallery").val();
    var url2 = jQuery("#hidWebPath").val() + "/Admin/service/postData.aspx?requestType=deleteasset&id=" + ID;
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: url2,
        success: function (response) {
            if (response != "" && response != 'undefined') {
                clearAll();
                fnBindFiles();
                $("#rightSideBox").hide();
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
