#                                   **Parkour2D**
# 初始化项目

* [场景资源](https://assetstore.unity.com/packages/2d/environments/2d-jungle-side-scrolling-platformer-pack-78506)
* [音效资源](https://assetstore.unity.com/packages/audio/sound-fx/sound-fx-retro-pack-121743)
* [人物资源](https://assetstore.unity.com/packages/2d/characters/knight-sprite-sheet-free-93897)

# 背景移动

* 通过`BgCtr,cs`控制背景的移动

# 地面移动与生成

* 通过`GroundCtr.cs`控制地面的移动
* 使用`GroundCtr.cs`调用`GroundItem.cs`实现随机地面生成



# 人物动画

* 使用关键帧生成人物动画
* 人物动画放入单例`AnimMan.cs`

#  游戏音效

* 将不同音效放入`AudioMan.cs`中统一管理

#  游戏结束

因为`Player`的摄像机层级是20，默认生成的`canvas`的层级是0，所以`GameUI`是新建的`canvas`，

并设置他的层级是20