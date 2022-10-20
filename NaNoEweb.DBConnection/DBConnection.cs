using NaNoEweb.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace NaNoEweb.DBConnection;

public static class DB
{
    public static DBConnection? I { get; set; }
}

public class DBConnection
{
    private SqlConnection? _connection { get; set; } = null;
    public bool Connected { get; private set; } = false;
    public DBConnection()
    {
        if (null != DB.I) throw new Exception("can't create multiple instances.");
        DB.I = this;

        _connection = new SqlConnection("server=localhost;database=nanoe_web;user id=nanoe_web;password=nanoe_web;Trusted_Connection=yes;connection timeout=30;MultipleActiveResultSets=true");
        try
        {
            _connection.Open();
            Connected = true;
        }
        catch
        { }

        // DBInterop listen-for-clients-sync
    }

    /// <summary>
    /// Get all from params
    /// </summary>
    /// <typeparam name="T">Type to retrieve</typeparam>
    /// <param name="data">specified 'WHERE {}' or blank string for all</param>
    /// <returns>Observable collection of the type T</returns>
    /// <exception cref="Exception">Not connected</exception>"
    public ObservableCollection<T> Get<T>(string data)
    {
        if (!Connected) throw new Exception();

        var answer = new ObservableCollection<T>();
        var t = typeof(T).ToString().Split('.').Last();

        using (var cmd = new SqlCommand())
        {
            cmd.Connection = _connection;
            switch (t)
            {
                case "MNovelInstance":
                {
                    cmd.CommandText = $"SELECT [id], [title], [info], [_lastupdate] FROM [MNovelInstance] {data}";
                    using (var read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dynamic innerT = Convert.ChangeType(new MNovelInstance()
                            {
                                ID = (uint)read.GetInt32(0),
                                Title = DBInterop.ConvertFromSafeString(read.GetString(1)),
                                Info = DBInterop.ConvertFromSafeString(read.GetString(2)),
                                LastEditTime = DateTime.Parse(DBInterop.ConvertFromSafeString(read.GetString(3)))
                            }, typeof(T));
                            answer.Add(innerT);
                        }
                        read.Close();
                    }
                }
                break;
                case "MNovelContent":
                {
                    cmd.CommandText = $"SELECT [id], [novelinstance_id], [id_before], [id_after], [flag], [text], [_lastupdate] FROM [MNovelContent] {data}";
                    using (var read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dynamic innerT = Convert.ChangeType(new MNovelContent()
                            {
                                ID = (uint)read.GetInt32(0),
                                NoverlInstance_ID = read.GetInt32(1),
                                ID_Before = read.GetInt32(2),
                                ID_After = read.GetInt32(3),
                                Flag = DBInterop.ConvertFromSafeString(read.GetString(4)),
                                Text = DBInterop.ConvertFromSafeString(read.GetString(5)),
                                LastEditTime = DateTime.Parse(DBInterop.ConvertFromSafeString(read.GetString(6)))
                            }, typeof(T));
                            answer.Add(innerT);
                        }
                        read.Close();
                    }
                }
                break;
                case "TTracking":
                {
                    cmd.CommandText = $"SELECT [id],[novelinstance_id],[session_start],[session_end],[wordcount_start],[wordcount_end],[chapters_start],[chapters_end],[paragraphs_start],[paragraphs_end],[bookmarks_start],[bookmarks_end],[notes_start],[notes_end] FROM [TTracking] {data}";
                    using (var read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dynamic innerT = Convert.ChangeType(new TTracking()
                            {
                                ID = (uint)read.GetInt32(0),
                                NovelInstance_ID = read.GetInt32(1),
                                SessionStart = DateTime.Parse(DBInterop.ConvertFromSafeString(read.GetString(2))),
                                SessionStop = DateTime.Parse(DBInterop.ConvertFromSafeString(read.GetString(3))),
                                WordCount_Start = read.GetInt32(4),
                                WordCount_End = read.GetInt32(5),
                                Chapters_Start = read.GetInt32(6),
                                Chapters_End = read.GetInt32(7),
                                Paragraphs_Start = read.GetInt32(8),
                                Paragraphs_End = read.GetInt32(9),
                                Bookmarks_Start = read.GetInt32(10),
                                Bookmarks_End = read.GetInt32(11),
                                Notes_Start = read.GetInt32(12),
                                Notes_End = read.GetInt32(13),
                            }, typeof(T));
                            answer.Add(innerT);
                        }
                        read.Close();
                    }
                }
                break;
            }
        }

        return answer;
    }

    public TTracking GetLatestTracking(int novelId)
    {
        int id = 0;
        using (var cmd = new SqlCommand())
        {
            cmd.Connection = _connection;
            cmd.CommandText = $"SELECT MAX([id]) FROM [TTracking] WHERE [novelinstance_id]={novelId} GROUP BY [id] ORDER BY [id] DESC";
            try
            {
                id = (int)cmd.ExecuteScalar();
            }
            catch { return null; }
        }
        return Get<TTracking>($"WHERE [id]={id}").First();
    }

    /// <summary>
    /// Update an object
    /// </summary>
    /// <typeparam name="T">The object to update</typeparam>
    /// <param name="update"></param>
    /// <exception cref="Exception">Not connected</exception>"
    /// <exception cref="NotImplementedException">Not implemented</exception>
    public void Update<T>(T update)
    {
        if (!Connected) throw new Exception();

        var t = typeof(T).ToString().Split('.').Last();
        using (var cmd = new SqlCommand())
        {
            cmd.Connection = _connection;
            switch (t)
            {
                case "MNovelInstance":
                    var t1 = update as MNovelInstance;
                    if (t1.ID == 0) throw new ArgumentException();
                    t1.LastEditTime = DateTime.Now;
                    cmd.CommandText = $"UPDATE [MNovelInstance] SET [title] = '{DBInterop.ConvertToSafeString(t1.Title)}',[info]='{DBInterop.ConvertToSafeString(t1.Info)}',[_lastupdate]='{DBInterop.ConvertToSafeString(t1.LastEditTime.ToString())}' WHERE id={t1.ID};";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return;
                case "MNovelContent":
                    var t2 = update as MNovelContent;
                    if (t2.ID == 0) throw new Exception();
                    t2.LastEditTime = DateTime.Now;
                    // Ignore update novel id; ignore flag - rather delete if you dont want. duh
                    // TODO: Add Delete + Maybe Log Deletes? - as in can go back in 3 months and go 'I wrote a better thing here, what was it' kind of vibe.
                    cmd.CommandText = $"UPDATE [MNovelContent] SET [id_before]={t2.ID_Before}, [id_after]={t2.ID_After}, [text]='{DBInterop.ConvertToSafeString(t2.Text)}', [_lastupdate]='{t2.LastEditTime.ToString()}' WHERE id={t2.ID}";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return;
                case "TTracking":
                    var t3 = update as TTracking;
                    if (t3.ID == 0) throw new Exception();
                    cmd.CommandText = $"UPDATE [TTracking] SET [session_end]='{DBInterop.ConvertToSafeString(t3.SessionStop.ToString())}',[wordcount_end]={t3.WordCount_End},[chapters_end]={t3.Chapters_End},[paragraphs_end]={t3.Paragraphs_End},[bookmarks_end]={t3.Bookmarks_End},[notes_end]={t3.Notes_End} WHERE [id]={t3.ID}";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return;
            }
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Insert an object
    /// </summary>
    /// <typeparam name="T">Type to insert</typeparam>
    /// <param name="insert">The object to insert</param>
    /// <exception cref="Exception">Not connected</exception>
    /// <exception cref="NotImplementedException">Not implemented</exception>
    /// <exception cref="ArgumentException">Wasn't blank ID, use Update instead</exception>
    public void Insert<T>(T insert)
    {
        if (!Connected) throw new Exception();

        var t = typeof(T).ToString().Split('.').Last();

        using (var cmd = new SqlCommand())
        {
            cmd.Connection = _connection;
            switch (t)
            {
                /* NB: capitalisation matters for MSSQL */
                case "MNovelInstance":
                    var t1 = insert as MNovelInstance;
                    if (t1.ID != 0) throw new ArgumentException();
                    cmd.CommandText = $"INSERT INTO [MNovelInstance] ([title], [info], [_lastupdate]) VALUES ('{DBInterop.ConvertToSafeString(t1.Title)}','{DBInterop.ConvertToSafeString(t1.Info)}','{DBInterop.ConvertToSafeString(t1.LastEditTime.ToString())}');";
                    cmd.ExecuteNonQuery();
                    using (var updateId = new SqlCommand())
                    {
                        updateId.Connection = _connection;
                        updateId.CommandText = $"SELECT MAX(id) FROM [MNovelInstance]";
                        t1.ID = (uint)((int)updateId.ExecuteScalar());
                    }
                    return;
                case "MNovelContent":
                    var t2 = insert as MNovelContent;
                    if (t2.ID != 0) throw new ArgumentException();
                    cmd.CommandText = $"INSERT INTO [MNovelContent] ([novelinstance_id],[flag],[id_before],[id_after],[text],[_lastupdate]) VALUES ({t2.NoverlInstance_ID},'{t2.Flag}',{t2.ID_Before},{t2.ID_After},'{DBInterop.ConvertToSafeString(t2.Text)}','{t2.LastEditTime.ToString()}')";
                    cmd.ExecuteNonQuery();
                    using (var updateId = new SqlCommand())
                    {
                        updateId.Connection = _connection;
                        updateId.CommandText = $"SELECT MAX(id) FROM [MNovelContent]";
                        t2.ID = (uint)((int)updateId.ExecuteScalar());
                    }
                    return;
                case "TTracking":
                    var t3 = insert as TTracking;
                    if (t3.ID != 0) throw new ArgumentException();
                    cmd.CommandText = $"INSERT INTO [TTracking] ([novelinstance_id],[session_start],[session_end],[wordcount_start],[wordcount_end],[chapters_start],[chapters_end],[paragraphs_start],[paragraphs_end],[bookmarks_start],[bookmarks_end],[notes_start],[notes_end]) VALUES ({t3.NovelInstance_ID},'{DBInterop.ConvertToSafeString(t3.SessionStart.ToString())}','{DBInterop.ConvertToSafeString(t3.SessionStop.ToString())}',{t3.WordCount_Start},{t3.WordCount_End},{t3.Chapters_Start},{t3.Chapters_End},{t3.Paragraphs_Start},{t3.Paragraphs_End},{t3.Bookmarks_Start},{t3.Bookmarks_End},{t3.Notes_Start},{t3.Notes_End})";
                    cmd.ExecuteNonQuery();
                    using (var updateId = new SqlCommand())
                    {
                        updateId.Connection = _connection;
                        updateId.CommandText = $"SELECT MAX(id) FROM [TTracking]";
                        t3.ID = (uint)((int)updateId.ExecuteScalar());
                    }
                    return;
            }
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Get the full map of the novel to build the page
    /// </summary>
    /// <param name="novelId">The ID of the novel to load the map for (once of need)</param>
    /// <returns>A collection to go through in order of 'where user is on novel'</returns>
    public ObservableCollection<DParagraphMap> GetNovelMap(string novelId)
    {
        var answer = new ObservableCollection<DParagraphMap>();
        using (var command = new SqlCommand($"SELECT COUNT(id) FROM [MNovelContent] WHERE [novelinstance_id]={novelId}", _connection))
        {
            var testCount = (int)command.ExecuteScalar();
            if (testCount == 0) return answer;

        }

        using (var query = new SqlCommand($"SELECT [id],[id_before],[id_after],[flag] FROM [MNovelContent] WHERE [novelinstance_id]={novelId}", _connection))
        {
            List<DParagraphMap> orderMe = new List<DParagraphMap>();
            using (var reader = query.ExecuteReader())
            {
                while (reader.Read())
                {
                    orderMe.Add(new DParagraphMap()
                    {
                        ID = reader.GetInt32(0),
                        ID_Before = reader.GetInt32(1),
                        ID_After = reader.GetInt32(2),
                        Flag = reader.GetString(3)
                    });
                }
                reader.Close();

                if (orderMe.Count > 0)
                {
                    var first = orderMe.Where(a => a.ID_Before == -1).First();
                    orderMe.Remove(first);

                    answer.Add(first);
                    var mostRecent = first;
                    while (orderMe.Count > 0)
                    {
                        var next = orderMe.Where(a => a.ID == mostRecent.ID_After).First();
                        orderMe.Remove(next);
                        answer.Add(next);
                        mostRecent = next;
                    }
                }
            }
        }

        return answer;
    }

    /// <summary>
    /// Get an array of counts for novel ID in order of:
    ///  - Paragraphs
    ///  - Chapters
    ///  - Notes
    ///  - Bookmarks
    ///  - Word Count
    /// </summary>
    /// <param name="id">ID of Novel we're counting Content for</param>
    /// <returns>Ordered list of Counts</returns>
    public int[] GetNumbers(int id)
    {
        int[] answer = new int[5];

        using (var cmd = new SqlCommand($"SELECT COUNT(id) as answer FROM MNovelContent WHERE novelinstance_id={id}", _connection))
        {
            var safetyNet = (int)cmd.ExecuteScalar();
            if (safetyNet == 0) return answer;
        }

        using (var cmd = new SqlCommand())
        {
            cmd.Connection = _connection;
            cmd.CommandText =
                $"SELECT" +
                $"(SELECT COUNT(id) FROM MNovelContent WHERE novelinstance_id={id} AND flag='P') as Paragraphs," +
                $"(SELECT COUNT(id) FROM MNovelContent WHERE novelinstance_id={id} AND flag='C') as Chapters," +
                $"(SELECT COUNT(id) FROM MNovelContent WHERE novelinstance_id={id} AND flag='N') as Notes," +
                $"(SELECT COUNT(id) FROM MNovelContent WHERE novelinstance_id={id} AND flag='B') as Bookmarks," +
                $"(SELECT SUM([dbo].[WordCount](text)) FROM MNovelContent WHERE novelinstance_id={id} AND flag='P') as WordCount";
            using (var queryAnswer = cmd.ExecuteReader())
            {
                queryAnswer.Read();

                answer[0] = queryAnswer.IsDBNull(0) ? 0 : queryAnswer.GetInt32(0);
                answer[1] = queryAnswer.IsDBNull(1) ? 0 : queryAnswer.GetInt32(1);
                answer[2] = queryAnswer.IsDBNull(2) ? 0 : queryAnswer.GetInt32(2);
                answer[3] = queryAnswer.IsDBNull(3) ? 0 : queryAnswer.GetInt32(3);
                answer[4] = queryAnswer.IsDBNull(4) ? 0 : queryAnswer.GetInt32(4);

                queryAnswer.Close();
            }
        }

        return answer;
    }

    /// <summary>
    /// Clean up
    /// </summary>
    ~DBConnection()
    {
        _connection?.Close();
    }
}