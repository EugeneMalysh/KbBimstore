
using System;
using System.Threading;

namespace KbBimstore
{

   public enum RequestId : int
   {
       Zero = 0,
       One = 1,
   }

   public class KbBimstoreRequest
   {
       private int m_request = (int)RequestId.Zero;

      public RequestId Take()
      {
          return (RequestId)Interlocked.Exchange(ref m_request, (int)RequestId.Zero);
      }

      public void Make(RequestId request)
      {
         Interlocked.Exchange(ref m_request, (int)request);
      }
   }
}
