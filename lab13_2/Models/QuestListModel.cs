using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab13_2.Models
{
    public class QuestListModel
    {
        public List<QuestModel> quests { get; set; }
        public int Count => quests.Count;
        public int CorrectAnswerCount
        {
            get
            {
                return quests.Count(q => q.AnswerIsCorrect);
            }
        }
    }
}
