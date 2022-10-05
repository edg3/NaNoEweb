<doctype !html>
    <html>

    <head>
        <title>Experiment Writing</title>
        <script src="../_jquery-3.6.0.js"></script>
        <script src="script.js"></script>
        <link rel="stylesheet" href="style.css">
        </link>
    </head>

    <body>
        <div class="novel-above">
            <div class="paragraph" data-id="50" data-prev="-1" data-next="51">
                <div class="row">
                    <div class="col-10">
                        above
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="51" data-prev="50" data-next="52">
                <div class="row">
                    <div class="col-10">
                        above
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="52" data-prev="51" data-next="53">
                <div class="row">
                    <div class="col-10">
                        above
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="53" data-prev="52" data-next="54">
                <div class="row">
                    <div class="col-10">
                        above
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="novel-writing">
            <div class="writing-area">
                <div class="row">
                    <div class="col-10">
                        <textarea class="writing-box"></textarea>
                    </div>
                    <div class="col-2">
                        <div class="action disabled" action="delete">Delete</div>
                        <div class="note"><strong>&lt;enter&gt;</strong> to <strong>save what's written</strong></div>
                        <div class="note"><strong>Normal</strong> text = <strong>Paragraph</strong></div>
                        <div class="note">'<strong>C:&lt;title&gt;</strong>' mark <strong>Chapter</strong>, title optional</div>
                        <div class="note">'<strong>N:&lt;text&gt;</strong>' make <strong>Note</strong>, needs text for info</div>
                        <div class="note">'<strong>B:&lt;name&gt;</strong>' mark <strong>Bookmark</strong>, needs name</div>
                        <div class="note"><input type="checkbox" class="edit-box"/> <strong>Edit Mode</strong></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="novel-below">
            <div class="paragraph"  data-id="54" data-prev="53" data-next="55">
                <div class="row">
                    <div class="col-10">
                        below
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="55" data-prev="54" data-next="56">
                <div class="row">
                    <div class="col-10">
                        below
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="56" data-prev="55" data-next="57">
                <div class="row">
                    <div class="col-10">
                        below
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>
            <div class="paragraph" data-id="57" data-prev="56" data-next="-2">
                <div class="row">
                    <div class="col-10">
                        below
                    </div>
                    <div class="col-2">
                        <div class="action" action="jump">Edit</div>
                        <div class="action" action="delete">Delete</div>
                        <div class="action" action="write-after">After</div>
                    </div>
                </div>
            </div>

        </div>
    </body>

    </html>