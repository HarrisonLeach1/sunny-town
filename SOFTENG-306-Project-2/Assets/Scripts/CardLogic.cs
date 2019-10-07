using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class CardLogic : MonoBehaviour
    {
        public GameObject card;

        private SpriteRenderer sr;

        // Start is called before the first frame update
        void Start()
        {
            sr = card.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                card.transform.position = pos;
            }
        }
    }
}