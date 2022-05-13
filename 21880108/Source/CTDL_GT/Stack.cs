using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTDL_GT
{
    public class Stack
    {
        Node head;
        Node tail;
        public Stack()
        {
            this.head = null;
            this.tail = null;
        }
        // thêm dữ liệu vào Stack
        public void Push(string value)
        {
            Node newNode = new Node(value);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.next = head;
                head = newNode;
            }
        }

        // lấy dữ liệu từ Stack
        public string Pop()
        {
            if (isEmty())
            {
                throw new Exception("Queue is Emty");
            }
            string value = head.value;
            head = head.next;
            return value;
        }

        public string Gethead()
        {
            if (isEmty())
            {
                throw new Exception("Queue is Emty");
            }
            return head.value;
        }
        public bool isEmty()
        {
            if (head == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
