using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOD4SLG
{
    public class LODSystem
    {
        public Camera camera;

        public Dictionary<LODLevel, LODChunk> LodStates = new Dictionary<LODLevel, LODChunk>();

        /// <summary>
        /// 相机的基础高度 H0.
        /// </summary>
        public float H0 = 10;

        public const float LeranceScale = 0.7f;

        public const float Lod0Size = 3.2f;

        public Action<LODLevel, int, int> OnLoadNewVisibleLodChunks;
        public Action<LODLevel, int, int> OnUnLoadInVisibleLodChunks;


        public void Update()
        {
            if (camera == null)
            {
                return;
            }

            var cameraPos = camera.transform.position;
            var centerPos = cameraPos;

            for (int lod = 0; lod < (int)LODLevel.MAX; lod++)
            {
                if (cameraPos.y > H0 * (2 ^ lod))
                {
                    continue;
                }

                LODLevel lodLevel = (LODLevel)lod;

                float tolerance = LeranceScale * GetLodChunkSize(lodLevel);
                var oldCenterPos = ChunkIdToWorldPos(lodLevel, LodStates[lodLevel].centerChunkId);
                if (Vector3.Distance(oldCenterPos, centerPos) > tolerance)
                {
                    var newCenterId = AlignToNearestChunkCenter(lodLevel, centerPos);
                    var oldCenterId = LodStates[lodLevel].centerChunkId;

                    LodStates[lodLevel].centerChunkId = newCenterId;

                    OnLoadNewVisibleLodChunks?.Invoke(lodLevel, oldCenterId, newCenterId);
                    OnUnLoadInVisibleLodChunks?.Invoke(lodLevel, newCenterId, oldCenterId);
                }
            }
        }

        public float GetLodChunkSize(LODLevel lodLevel)
        {
            return Lod0Size * (2 ^ (int)lodLevel);
        }

        public Vector2 ChunkIdToWorldPos(LODLevel lodLevel, int chunkId)
        {
            // TODO
            return Vector2.zero;
        }

        public int AlignToNearestChunkCenter(LODLevel lodLevel, Vector2 pos)
        {
            // TODO
            return 0;
        }
    }
}