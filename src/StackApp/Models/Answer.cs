using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackApp.Models
{
    [Table("Answers")]
    public class Answer: Post
    {
        [Key]
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        public bool ChosenAnswer { get; set; }
        public Answer ()
        {
            ChosenAnswer = false;
        }
    }
}
