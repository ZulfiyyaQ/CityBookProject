let commentArea = document.querySelector(".textarea");
let star1 = document.querySelector("#rating-1");
let star2 = document.querySelector("#rating-2");
let star3 = document.querySelector("#rating-3");
let star4 = document.querySelector("#rating-4");
let star5 = document.querySelector("#rating-5");
let starcount = 0;

let commentSubmit = document.querySelector('.commentsubmit')
commentSubmit.addEventListener("click", function (event) {
    event.preventDefault();
})
star1.addEventListener("click", function () {
    starcount = 1;
    console.log("Star Count:", starcount);
});

star2.addEventListener("click", function () {
    starcount = 2;
    console.log("Star Count:", starcount);
});

star3.addEventListener("click", function () {
    starcount = 3;
    console.log("Star Count:", starcount);
});

star4.addEventListener("click", function () {
    starcount = 4;
    console.log("Star Count:", starcount);
});

star5.addEventListener("click", function () {
    starcount = 5;
    console.log("Star Count:", starcount);
});

let CommentWrapper = document.querySelector(".reviews-comments-wrap")
let CommentUserName = document.querySelector(".comment-user-name")
let CommentUserImg = document.querySelector(".add-comment-userimg")
async function CommentSubmitHandler(button) {
    let id = button.id;
    let comment = commentArea.value;

    await fetch(`https://localhost:7296/Listing/Review?id=${id}&rating=${starcount}&comment=${comment}`)
    CommentWrapper.innerHTML += `
    <div class="reviews-comments-item">
    <div class="review-comments-avatar">
        <img src="/images/${CommentUserImg.textContent}" alt="${CommentUserName.textContent}">
    </div>
    <div class="reviews-comments-item-text">
        <h4><a href="#">${CommentUserName.textContent}</a></h4>
        <div class="listing-rating card-popup-rainingvis" data-starrating2="${starcount}"> </div>
        <div class="clearfix"></div>
        <p>"${comment}"</p>
    </div>
</div>
    `
}
