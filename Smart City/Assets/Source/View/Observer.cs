using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Observer{
    public interface Observer : MonoBehaviour
    {
        public void reactTo();
    }
}
