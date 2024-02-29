using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class MovingHandler : MonoBehaviour
    {
        public GameObject movableObject = null;
        Vector3 mousePosition;
        Vector3 mouseOffset;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) == true)
            {
                Collider2D[] targetsHit = Physics2D.OverlapPointAll(mousePosition);
                Collider2D targetHit = GetTopCardFromColiders(targetsHit);
                if(targetHit != null) {
                    movableObject = targetHit.transform.gameObject;
                    mouseOffset = movableObject.transform.position - mousePosition;
                }
            }

            if(movableObject != null)
            {
                movableObject.transform.position = mousePosition + mouseOffset;
            }

            if (Input.GetMouseButtonUp(0) == true && movableObject != null)
            {
                movableObject = null;
            }
        }

        Collider2D GetTopCardFromColiders( Collider2D[] coliders ) 
        {
            return coliders
                .Where(x => x.transform.gameObject.CompareTag("Card"))
                .OrderByDescending(x => x.transform.gameObject.GetComponent<Renderer>().sortingOrder)
                .FirstOrDefault();
        }
    }
}