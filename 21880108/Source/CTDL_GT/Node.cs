using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTDL_GT
{
    public class Node
    {
        public string value;
        public Node next;

        public Node(string value)
        {
            this.value = value;
            this.next = null;
        }
    }
}
