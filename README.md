# InfinityHuluMod

## Mod效果
使指定的一个或多个葫芦饮用时不消耗次数,并且在葫芦为空的情况下也可以饮用

注意: 需要黑神话:悟空游戏文件夹内已有CSharpLoader with hook才可使用该Mod
## CSharpLoader
Github仓库地址:https://github.com/czastack/B1CSharpLoader
N网地址:https://www.nexusmods.com/blackmythwukong/mods/664

# 项目使用和Mod使用
将该项目直接编译即可在编译输出目录中得到InfinityHulu.dll

将该dll按以下相对路径放在黑神话:悟空游戏目录中CSharLoader的Mods文件夹内即可被CSharpLoader识别并加载

目录: b1\Binaries\Win64\CSharpLoader\Mods\InfinityHuluMod\InfinityHulu.dll

[可以自己调整该项目的编译后处理事件，将批处理命令的目标文件夹设置为自己电脑的上述目录，可以在开发中省略手动复制的步骤]

### QA
问：饮用不消耗次数,那葫芦为空时也可以饮用还有什么意义?
答：游戏中有乾坤彩葫芦和博山炉这两个在酒量耗尽时有较大的加成的道具，我不希望因为这个Mod无法让大家发挥最强实力(doge)。

问：饮用不消耗次数，那我怎么把葫芦喝光来触发道具呢？
答：游戏中的酒量不会因为切换葫芦而变多，只会不变或者变少[当从大容量葫芦切换到小容量葫芦时，酒量会变化到不超过小葫芦容量]。那么就可以将青田葫芦不配置为无限的葫芦，喝完后再切换回自己需要的葫芦，就可以得到一个空葫芦来触发道具了。