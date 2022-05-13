using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTDL_GT
{
    public class Queue
    {
        Node head;
        Node tail;
        public Queue()
        {
            this.head = null;
            this.tail = null;
        }
        // thêm dữ liệu vào Queue
        public void Enqueue(string value)
        {
            Node newNode = new Node(value);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.next = newNode;
                tail = newNode;
            }
        }

        // lấy dữ liệu từ Queue
        public string Dequeue()
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
