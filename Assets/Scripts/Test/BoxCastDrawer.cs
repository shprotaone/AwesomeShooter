using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Infrastructure.ECS.Components;
using MonoBehaviours;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoxCastDrawer : MonoBehaviour
{
   [SerializeField] private Vector3 _offset;
   [SerializeField] private float _drawLenght;
   [SerializeField] private Vector3 _extends = new Vector3(2, 2, 1);
   private RaycastHit[] _results;

   [SerializeField] private EnemySpawnPoint[] _selectablePoints;

   private Stopwatch sw;
   private void Start()
   {
      _results = new RaycastHit[10];
      _selectablePoints = new EnemySpawnPoint[10];
   }

   /*public void FixedUpdate()
   {
      sw = new Stopwatch();
      sw.Start();

      _results = Physics.BoxCastAll(transform.position, _extends,transform.forward,
         Quaternion.identity,_drawLenght,Masks.SpawnPoint);

      if (_results.Length > 0)
      {
         for (int i = 0; i < _results.Length; i++)
         {
            var point = _results[i].transform.GetComponent<EnemySpawnPoint>();

            bool contains = CheckSelectable(point);
            if (!contains) _selectablePoints[i] = point;
         }
      }

      CheckUnselectableV2(_results.Length);
      sw.Stop();
      Debug.Log(sw.Elapsed);
   }

   private void CheckUnselectableV2(int index)
   {
      for (int i = index; i < _selectablePoints.Length; i++)
      {
         var point = _selectablePoints[i];

         if (point != null)
         {
            point.Disable();
         }

         _selectablePoints[i] = null;
      }
   }

   private bool CheckSelectable(EnemySpawnPoint spawnPoint)
   {
      if (!_selectablePoints.Contains(spawnPoint))
      {
         spawnPoint.SetWorks();
         return false;
      }
      return true;
   }*/

   public void OnDrawGizmos()
   {
      Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);

      // Convert the local coordinate values into world
      // coordinates for the matrix transformation.
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawCube(transform.localPosition + Vector3.forward * (_drawLenght / 2),new Vector3(_extends.x,_extends.y,_drawLenght));
      Gizmos.DrawRay(transform.localPosition,Vector3.forward * _drawLenght);
   }
}