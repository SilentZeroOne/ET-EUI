using BM;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace ET
{
    public class SceneChangeComponentUpdateSystem: UpdateSystem<SceneChangeComponent>
    {
        public override void Update(SceneChangeComponent self)
        {
            if (self.loadMapOperation == null) return; 
            
            if (!self.loadMapOperation.isDone)
            {
                return;
            }

            if (self.tcs == null)
            {
                return;
            }
            
            ETTask tcs = self.tcs;
            self.tcs = null;
            tcs.SetResult();
        }
    }
	
    
    public class SceneChangeComponentDestroySystem: DestroySystem<SceneChangeComponent>
    {
        public override void Destroy(SceneChangeComponent self)
        {
            self.loadMapOperation = null;
            self.tcs = null;
        }
    }

    [FriendClass(typeof(SceneChangeComponent))]
    public static class SceneChangeComponentSystem
    {
        public static async ETTask ChangeSceneAsync(this SceneChangeComponent self, string sceneName,LoadSceneMode mode = LoadSceneMode.Single)
        {
            self.tcs = ETTask.Create(true);
            // 加载map

            await AssetComponent.LoadAsync<SpriteAtlas>(BPath.Assets_Bundles_ResBundles_Atlas_MapAltlas__spriteatlas);
            await AssetComponent.LoadSceneAsync(sceneName.SceneNameToAB());
            self.loadMapOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            //this.loadMapOperation.allowSceneActivation = false;
            await self.tcs;
        }

        public static int Process(this SceneChangeComponent self)
        {
            if (self.loadMapOperation == null)
            {
                return 0;
            }
            return (int)(self.loadMapOperation.progress * 100);
        }
    }
}