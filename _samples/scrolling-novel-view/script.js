$(document).ready(function(){
    $(document).bind('mousewheel',function(e){
        if (e.originalEvent.wheelDelta /120 > 0) { // up
            //console.log('scroll up');
            var dropDown = $('.novel-above .paragraph').last();
            $('.novel-above').remove(dropDown);
            $('.novel-below').prepend(dropDown);
        }
        else {
            //console.log('scroll down');
            var pushUp = $('.novel-below .paragraph').first();
            $('.novel-below').remove(pushUp);
            $('.novel-above').append(pushUp);
        }
    });
});