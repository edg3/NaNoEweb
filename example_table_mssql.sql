use nanoe_web;
/* Novels: Name + Info; e.g. 'NaNoWriMo 2022; short story about A, B, and C.' */
/* TODO: work out user for service perhaps? */
CREATE TABLE Novels(
   id int IDENTITY (1,1) NOT NULL,
   title VARCHAR(50) NOT NULL,
   info VARCHAR(MAX) NOT NULL,
   /*MySQL_ID int NOT NULL, : Android */
   _lastupdate VARCHAR(50) NOT NULL,
   );
GO
/*
Aim: Desktop Designed interaction flow

Start View - Novels list
 - New Novel
 - Import NaNoE file [will be hardcore since need to get it out, put in in correct order, then plop it here]
 - Import Word document
 [maybe: look at other formats needs to import?]
 [maybe: open last?]
 - Interactive search bar - can get something with name/info has word
 - Click on Novel opens info, has button 'Open Me', effectively

Writing View - Normal first view when novel opened [add last time was on position X to DB?]
 - Adaptive word counts flow up and down with scroll bar
 - Interactive section right side: list of all helpers - use JS interaction to interact, hot search bar, etc. Interact can open two with a tickbox so can swap between all/search/onlyticks
 - Interactive section on the left side: errors that does all the hardcore checks for spelling/grammer
 [maybe hilight words that are spelt incorrectly after 'space'/'enter'?]
 - A way to bookmark places
 - Make a cool way for 'C:<num>{<optional title>}' OR 'Chapter <num>{<optional title>}' line - enter => this is a chapter marked as such auto
 - Make a cool way for 'N:<notes>' on enter make an interactive note
 - Make a cool way for 'B:<title>' on enter make a bookmark for yourself
 - Make scoll up and down move to edits of things
 - Enter on something = new thing under
 - Make the interactive document movement will be most difficult, but I have an idea, at least
 [maybe a possiblity to see size of paragraph on screen and take it back till there is one out the top of page, and/or off bottom]
 [this way can read back and forth a bit for the big screen size]
 [make the loading and movement work well on DB through AJAX & Limits; -1 = first; -2 = last; so first item has that, kinda data interaction]
 - Make a 'jump' method for each para shown so I might notice 'para X has problem Y', click on it, it becomes interactive and does ajax above and below
 [maybe make this use an ajax oriented 'read to me' if I can, so you can have it read out to you]

Options View
 - Add your own rules for the spell/grammar checks like in the past.
 - Make a way to see 'work done on novel' and tracking around that as a whole
 [import from novel - e.g. next accidental distances, then can clean it and change it as needed; same for interactive]

Notes/Bookmarks/Chapters views


[BD Needs]
Paragraph[text,ch,bookmark,note]
Options[options,rules, as in can say here the banned words, the theme colour, etc, in interactive view]
Logs[novel - time bound, can be made interactive so can leave it and it doesnt track that time, so segments, then gets the approximate time typing the novel]
*/