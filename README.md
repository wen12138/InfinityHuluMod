# InfinityHuluMod

## Mod效果/Mod Effects
使指定的一个或多个葫芦饮用时不消耗次数,并且在葫芦为空的情况下也可以饮用
Make the gourds you config drink without consuming times, and can also drink when the gourds are empty

注意: 需要黑神话:悟空游戏文件夹内已有CSharpLoader with hook才可使用该Mod
Attention: This mod dependent on CSharpLoader with hook, you should download it in advance;
## CSharpLoader
Github仓库地址/Github Repositry:https://github.com/czastack/B1CSharpLoader
N网地址/Nexus Site:https://www.nexusmods.com/blackmythwukong/mods/664

## 项目使用和Mod使用 / How to use the project and mod
使用Visual Studio或Rider项目直接编译即可在编译输出目录中得到InfinityHulu.dll
Complie the project by using Visual Studio or Rider, and you can get "InfinityHulu.dll" in output path;

将该dll文件和InfinityHuluConfig目录下的InfinityHuluConfig.json文件按以下相对路径放在黑神话:悟空游戏目录中CSharLoader的Mods文件夹内即可被CSharpLoader识别并加载
Copy "InfinityHulu.dll" and "InfinityHuluConfig.json"(in "InfinityHuluConfig" folder) to "CSharpLoader/Mods" in the following relative path of Black Myth: Wukong game file.

目录/Path: 
b1\Binaries\Win64\CSharpLoader\Mods\InfinityHuluMod\InfinityHulu.dll
b1\Binaries\Win64\CSharpLoader\Mods\InfinityHuluMod\InfinityHuluConfig.json
[可以自己调整该项目的编译后处理事件，将批处理命令的目标文件夹设置为自己电脑的上述目录，可以在开发中省略手动复制的步骤]
[You can adjust the post-compile processing events of the project yourself, set the target folder of batch commands to the directory on your computer, and you can omit the manual copy step in development]
## 如何配置/How to config
在InfinityHuluConfig目录下有一个教程文本文件,你可以详细查看
There is a tutorial file in "InfinityHuluConfig" floder, you can view in detail

### QA
问: 饮用不消耗次数,那葫芦为空时也可以饮用还有什么意义?
Q: Drinking does not consume the number of times, what is the meaning of the gourd can drink wine when empty?
答: 游戏中有乾坤彩葫芦和博山炉这两个在酒量耗尽时有较大的加成的道具,我不希望因为这个Mod无法让大家发挥最强实力(doge)。
A: There are some items in game can make you have powerful buff when your gourd is empty. I don't want you can not show your true power becuse of my mod.

问: 饮用不消耗次数，那我怎么把葫芦喝光来触发道具呢？
Q: Drinking does not consume the number of times, how can I drink it to empty?
答: 你可以将青田葫芦不配置为无限的葫芦，喝完后再切换回自己需要的葫芦，就可以得到一个空葫芦来触发道具了。[这正是默认的配置]
A: You can keep the "only 1 time gourd" stay normal, so that you can drink it to empty and then switch to the gourd you like. [This is the deafult config]