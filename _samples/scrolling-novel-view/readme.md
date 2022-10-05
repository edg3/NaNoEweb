scrolling-novel-view
---
Idea:
 - Get all paragraph order IDs
 - Render on page as 'loading' using position for where we were last busy
   - goes from position backwards loading seperate thread to position forwards load as well
     as in X all the way X-1, X-2, till 0 the first thread; and separate X+1, X+2, maxnum on the second thread
     idea being it will be fast for the 'page ready' and user can start
   - might have trickery if the user pops to a bookmark as soon as they open the page
 - This way everything has writing ID already
 - If not edit-ticked it does updates as needed with last from above and first from below IDs
 - If yes edit-ticked it can hilight the paragraph above show suggested underlines, put text into edit box
   - Enter is save + refresh list above
   - Scroll up and down loads next, etc
   - Enter must be used to save so you can drop it by turning off edit
   - Yes ticked shows 'find next para with a problem' from the start to the end as an action button
 - This will likely be a fast long novel loader with quick userfriendly simple interaction for writing
   That is, ajax requests to web service to manage content movement, saving, etc, and can do -data-id binding 
   for ajax call to DB to add which would make DB send back 'this -ID update everywhere' to client 