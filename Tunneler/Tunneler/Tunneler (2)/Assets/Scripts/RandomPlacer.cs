using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RandomPlacer class
 * Inherited from ItemGenerator
 * GenerateItem(Pipe) spawns and randomly positions an item (Item object)
 */ 
public class RandomPlacer : ItemGenerator {

    public Item[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            Item item = Instantiate<Item>(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            float pipeRotation = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) * 360f / pipe.pipeSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
