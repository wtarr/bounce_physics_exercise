using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bounce_Physics
{
    interface IamASphere
    {
        bool HasCollisionOccured(Sphere g1, Sphere g2);

    }
}
