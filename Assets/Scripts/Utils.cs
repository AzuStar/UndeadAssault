using System;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadAssault
{
    public static class Utils
    {
        public static List<Entity> GetEntitiesInRadius(
            Vector3 position,
            float radius,
            Predicate<Entity> predicate
        )
        {
            // use Physics.OverlapSphere to get all colliders in a sphere
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            List<Entity> entities = new List<Entity>();
            foreach (Collider collider in colliders)
            {
                Entity entity = collider.GetComponent<Entity>();
                if (entity != null && predicate(entity))
                {
                    entities.Add(entity);
                }
            }
            return entities;
        }
    }
}
