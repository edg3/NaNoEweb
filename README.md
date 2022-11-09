NaNoE.web
---
Web implementation of NaNoE; putting my Asp.Net skills to best practice.
- Idea: make it a web service, then can also use my phone to write/edit my novel(s)

Using 'System.Data.SqlClient' for 'NaNoEweb.DBConnection' web service

USing 'sqlite-net-pcl' for 'NaNoEwebAndroid.DBConnection' client service

Note: create fastest data sharing method for phones <-> web updates

4:30am reminder for myself: ctrl+K - ctrl+D

---
2022.10.21 - TODO ASAP - my list of tasks keeps stealing me away from this work, lol
---
- End sentence characters:
	.	.�	?�	!�	?	!
- Bug: notes interaction, as a whole
- close 'add note/chapter/note note/chapter note' well with interactions - keep data but hide, perhaps?

Get Errors suggestions up in edit mode:
- Spelling
- Grammar
- Own 'hot edit suggestions' system and setup, e.g. I don't want to use the word 'because' so I mark it as spam
- Hot search for area that needs edits
- Method to mark area as 'ignore' for all future searches

Test:
- Do a writing challenge

---
Completed:
- Sorted deletes and visible counts updates

Chapters Helper:
- Add in top right chapter notes section (ajax, jquery, and partials/controller functions)
- [cancelled, dont need on chapters] Make search function to go through long lists faster

Notes Helper:
- Add in top right notes note section (ajax, jquery, and partials/controller functions)
- Make search function to go through long lists faster
- Make some sort of suggesting to use 'CategoryName:' for ordering and collections, then can search for 'CategoryName' and it shows all, e.g. 'People:' search shows all categorised as 'People:' - hints all thats needed; you can structure it for yourself