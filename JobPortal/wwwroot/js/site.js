// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//$(document).on('click', '.job-list-card', function () {
//    $('.job-list-card').removeClass('card-active');
//    $(this).addClass('card-active');
//})
//$(document).on('click', '#login-page', function () {
//    $(this).addClass('active').siblings().removeClass('active');
//})
//$(document).on('click', '.jsx-143433104', function () {
//    $('.jsx-143433104').removeClass('btn-active');
//    $(this).addClass('btn-active');
//})
// Write your JavaScript code.
function changeActor() {
    document.getElementById("actorname").textContent = "NGƯỜI TÌM VIỆC";
    document.getElementById("actorinfo").textContent = "Tìm việc làm online";
}
////declearing html elements

//const imgDiv = document.querySelector('.profile-pic');
//const img = document.querySelector('#avatar');
//const file = document.querySelector('#Input_UploadAvatar');
//const uploadBtn = document.querySelector('#uploadBtn');

////if user hover on img div 

//imgDiv.addEventListener('mouseenter', function () {
//    uploadBtn.style.display = "block";
//});

////if we hover out from img div

//imgDiv.addEventListener('mouseleave', function () {
//    uploadBtn.style.display = "none";
//});

////lets work for image showing functionality when we choose an image to upload

////when we choose a foto to upload

//file.addEventListener('change', function () {
//    //this refers to file
//    const choosedFile = this.files[0];

//    if (choosedFile) {

//        const reader = new FileReader(); //FileReader is a predefined function of JS

//        reader.addEventListener('load', function () {
//            img.setAttribute('src', reader.result);
//        });

//        reader.readAsDataURL(choosedFile);
//    }
//});
