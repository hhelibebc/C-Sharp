using System;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class GameEntry : Form
    {
        string[] default_strs =
        {
            "此处省略，并且不打算加上了。",
        };
        string[] trade_dialogs =
        {
            "感谢你救了我！我可以用魔法为你提升 3% 的攻击力和防御力，请问是现在就提升吗？",
            "我有 很多 黄钥匙，你出 150 个金币我就卖给你 1 把。",
            "我愿意以每把 100 个金币的价格收购黄钥匙，你需要吗？",
            "你可以在我这儿花 1000 金币提升 2000 生命值。",
            "我有 1 个地震卷轴，你出 4000 个金币我就卖给你，怎样？"
          };
        string[] dialog1 =
        {
            "感谢你救了我！我这里有 1000 枚金币作为答谢，请你务必收下。"
        };
        string[] dialog2 =
        {
            "你清醒了吗？你到监狱时还处在昏迷中，魔法警卫把你扔到我这个房间" +
            "但你很幸运，我刚完成逃跑的暗道你就醒了，我们一起越狱吧。",
            "我们终于逃出来了，你的剑和盾被警卫拿走了，你必须先找到武器。我知道" +
            "铁剑在 5 楼，铁盾在 9 楼，你最好先取到他们。我现在还有事要做没法帮你，再见。"
        };
        string[] dialog3 =
        {
            "魔王：欢迎来到魔塔，你是第一百位挑战者。如果你能打败我所有的手下，我就与你" +
            "一对一的决斗。现在你必须接受我的安排。",
            "什么？"
        };
        string[] dialog4 =
        {
            "---------------",
            "喂！醒醒！"
        };
        string[] dialog5 =
        {
            "我能给你一本怪物手册，你可以鼠标左键单击使用它。他能为你计算当前楼层各类怪物对你的伤害。",
        };
        string[] dialog6 =
        {
            "不许动，你已经被包围了！",
            "等你打得过我的手下再来找我吧。"
        };
        string[] dialog7 =
        {
            "你居然能突出重围？来吧，看看我们俩谁更厉害！",
        };
        string[] dialog8 =
        {
            "好吧，算你厉害！。",
            "别大意，我只是一个小喽啰，魔塔里比我强的不计其数，会有人打败你的。",
        };
        string[] dialog9 =
        {
            "你运气真好！",
            "我刚挖开这个通道你就赶上了。再见！"
        };
        string[] dialog10 =
        {
            "你竟然能打到我这里来？看来是小看你了。",
            "来吧，让我称称你的斤两。"
        };
        string[] dialog11 =
        {
            "不可能！你为什么能打败我。",
            "好吧，我败了。你可以从这儿上去了。"
        };
        string[] dialog12 =
        {
            "公主，我来救你了。跟我一起走吧！",
            "什么？这只是个洋娃娃！！！。"
        };
        string[] dialog13 =
        {
            "我们又见面了。",
            "你每次都这么及时，这次的通道你也可以免费使用。"
        };
        string[] dialog14 =
        {
            "听说你把下面两个区域的首领都打败了？",
            "来跟我打一场吧！",
            "如果你能胜过我，我就放你通过我负责的区域。"
        };
        string[] dialog15 =
        {
            "不，不，不!",
            "我不可能输给你的。",
            "肯定是我今天状态不好，之后重新再打一场。我在40层等你"
        };
        string[] dialog16 =
        {
            "感谢你救了我！",
            "我现在就去35楼挖开一条通道，现在的你绝对打不过魔龙，必须绕开它。"
        };
        string[] dialog17 =
        {
            "你终于来了！通道已经挖好了。",
            "我要离开这该死的魔塔了，太危险了!祝你好运。"
        };
        string[] dialog18 =
        {
            "你居然打败了我所有的手下。我跟你拼了！"
        };
        string[] dialog19 =
        {
            "呼~好险，差点被你杀了",
            "算你厉害，我先溜了。你可以通过这儿了"
        };
        string[] dialog20 =
        {
            "骑士队长：你怎么又来了？勇士饶命！",
            "魔王：放肆！！！",
            "魔王：你居然敢临阵脱逃？",
            "骑士队长：大王，饶了我吧。我实在是打不过这位勇士。",
            "魔王：我们魔塔不需要这种废物！魔法警卫给我上，杀了这个逃兵。",
            "骑士队长：啊！！！",
            "魔王：虽然我刚刚态度有点异常，但现在是不会对你出手的，我会在50层等你"
        };
        string[] dialog21 =
        {
            "勇者：你怎么会在这儿？你不是已经离开魔塔了吗？",
            "小偷(误)：我会出现在这儿的原因，只有一个。那就是：",
            "魔王：我就是魔王。外面的公主、小偷什么的都是假的。",
            "勇者：啊？那你之前为什么多次帮助我，让我能顺利绕开强敌？",
            "魔王：我的武器是智慧权杖，你的武器是神圣剑。但我无法掌握神圣剑的力量，" +
            "我在等一个能掌握神圣剑的勇者——就是你。你之前的力量太过弱小，无法掌控" +
            "神圣剑，因此给你设置了那些障碍。我们合作吧，甚至可以统治世界！这样就没有" +
            "掌控之外的力量了。",
            "勇者：我是不会让你得逞的！来战斗吧，我一定会打败你。"
        };
        string[] dialog22 =
        {
            "恭喜你！征服了50层魔塔。" +
            "就此结束。我已经尽力了。尽力复现了里面必要的故事情节、对话。" +
            "但还是删掉了在商人处购买商品后的对话，无实际好处的智者对话，高级巫师的回避特性，"+
            "以及所有动画效果。代码量巨大，实在想不出该怎么继续优化了。"
        };

        const byte INDEXOF_BLANK = 0, INDEXOF_IRON_DOOR = 6, INDEXOF_THIEF = 16,
            INDEXOF_SKELETON = 61,// 骷髅战士62，骷髅队长63
            INDEXOF_KNIGHT_LEADER = 71,
            INDEXOF_UPSTAIR = 11,// 下行楼梯10
            INDEXOF_RED_DRAG = 20;// 蓝药水,红蓝宝石，红黄蓝钥匙

        void GeneralTalkDialog(string[] strs)
        {
            talk_dialog.Init(strs);
            talk_dialog.ShowDialog();
        }
        bool SpecialTradeEvent(int index, int cash, int cost)
        {
            trade_dialog.Init(trade_dialogs[index], cash, cost);
            trade_dialog.ShowDialog();
            return trade_dialog.GetReturn() == 1;
        }
        bool PointDetect(int r, int c)
        {
            return (game.row != r) || (game.col != c);
        }
        bool PointDetectEx(int r, int c, int target)
        {
            return game.map[game.layer, r, c] == target;
        }
        void GeneralDefendEvent(POINT detect, POINT[] defender, POINT[] action)
        {
            if (PointDetect(detect.r, detect.c))
                return;
            int i = 0;
            for (i = 0; i < defender.Length; i++)
            {
                if (!PointDetectEx(defender[i].r, defender[i].c, INDEXOF_BLANK))
                    return;
            }
            for (i = 0; i < action.Length; i++)
                UpdateMapEx(game.layer, action[i].r, action[i].c, INDEXOF_BLANK);
        }
        void GeneralDefendEvent(POINT detect, DIRECTION defender, DIRECTION action)
        {
            int ind1 = 0, ind2 = 0, ind3 = 0;
            GetRound(detect.r, detect.c);
            if (defender == DIRECTION.HORIZONTAL)
            {
                ind1 = 3;
                ind2 = 5;
            }
            else if (defender == DIRECTION.VERTICAL)
            {
                ind1 = 1;
                ind2 = 7;
            }
            switch (action)
            {
                case DIRECTION.LEFT: ind3 = 3; break;
                case DIRECTION.RIGHT: ind3 = 5; break;
                case DIRECTION.UP: ind3 = 1; break;
                case DIRECTION.DOWN: ind3 = 7; break;
                default: break;
            }
            GeneralDefendEvent(detect, new POINT[] { pts[ind1], pts[ind2] }, new POINT[] { pts[ind3] });
        }
        void GeneralTradeEvent(POINT pt, int cost, int redkey, int bluekey, int yellowkey)
        {
            string str = "我这儿有：\n";
            if (redkey > 0)
                str += redkey.ToString() + " 把红钥匙\n";
            if (bluekey > 0)
                str += bluekey.ToString() + " 把蓝钥匙\n";
            if (yellowkey > 0)
                str += yellowkey.ToString() + " 把黄钥匙\n";
            str += "你只要出 " + cost.ToString() + " 枚金币我就卖给你。";
            trade_dialog.Init(str, game.braver.gold, cost);
            trade_dialog.ShowDialog();
            if (trade_dialog.GetReturn() == 1)
            {
                UpdateMapEx(game.layer, pt.r, pt.c, INDEXOF_BLANK);
                game.braver.gold -= cost;
                game.RedKeyCount += (byte)redkey;
                game.YellowKeyCount += (byte)yellowkey;
                game.BlueKeyCount += (byte)bluekey;
                UpdateKeysView();
                UpdateBraverProperty();
            }
        }
        void L2_R2_C6()// 中级士兵
        {
            GeneralDefendEvent(new POINT(2, 7),
                new POINT[] { new POINT(2, 6), new POINT(2, 8) },
                new POINT[] { new POINT(5, 5), new POINT(8, 5), new POINT(11, 5), new POINT(5, 9), new POINT(8, 9), new POINT(11, 9) });
        }
        void L2_R4_C11()// 智者
        {
            GeneralTalkDialog(dialog1);
            UpdateMapEx(2, 4, 11, INDEXOF_BLANK);
            game.braver.gold += 1000;
            UpdateBraverProperty();
        }
        void L2_R7_C11()// 商人
        {
            if (SpecialTradeEvent(0, 1, 0))
            {
                UpdateMapEx(2, 7, 11, INDEXOF_BLANK);
                game.braver.attack = (int)(game.braver.attack * 1.03);
                game.braver.defense = (int)(game.braver.defense * 1.03);
                UpdateBraverProperty();
            }
        }
        void L2_R7_C3()// 小偷
        {
            if (PointDetectEx(8, 5, INDEXOF_IRON_DOOR))
            {
                GeneralTalkDialog(dialog2);
                UpdateMapEx(2, 7, 3, INDEXOF_BLANK);
                UpdateMapEx(2, 7, 2, INDEXOF_BLANK);
                UpdateMapEx(15, 1, 9, INDEXOF_THIEF);
            }
            else
            {
                GeneralTalkDialog(dialog16);
                UpdateMapEx(2, 7, 3, INDEXOF_BLANK);
                UpdateMapEx(35, 9, 5, INDEXOF_THIEF);
            }
        }
        void L3_R9_C5()// 魔王
        {
            if (game.row == 9 &&
                game.col == 5 &&
                game.braver.life == 1000 &&
                game.braver.attack == 100 &&
                game.braver.defense == 100)
            {
                UpdateMap(6, 5, "魔王");
                UpdateMap(8, 5, "魔法警卫");
                UpdateMap(10, 5, "魔法警卫");
                UpdateMap(9, 4, "魔法警卫");
                UpdateMap(9, 6, "魔法警卫");
                GeneralTalkDialog(dialog3);
                game.braver.life = 400;
                game.braver.attack = 10;
                game.braver.defense = 10;
                game.layer = 2;
                game.row = 8;
                game.col = 3;
                UpdateFloor();
                UpdateBraverProperty();
                UpdateText(8, "无");
                UpdateText(9, "无");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 5, "空白");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 4, "空白");
                GeneralTalkDialog(dialog4);
            }
        }
        void L3_R4_C11()// 智者
        {
            GeneralTalkDialog(dialog5);
            UpdateMapEx(3, 4, 11, INDEXOF_BLANK);
            GetProp("怪物手册");
        }
        void L6_R4_C8()// 商人
        {
            GeneralTradeEvent(new POINT(4, 8), 50, 0, 1, 0);
        }
        void L7_R1_C6()// 商人
        {
            GeneralTradeEvent(new POINT(1, 6), 50, 0, 0, 5);
        }
        void L8_R5_C10()// 初级士兵
        {
            GeneralDefendEvent(new POINT(5, 10), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L10_R5_C6()// 骷髅队长
        {
            if (PointDetect(5, 6))
                return;
            if (game.map[10, 4, 6] == INDEXOF_SKELETON + 2)
            {
                GeneralTalkDialog(dialog6);
                UpdateMapEx(10, 3, 1, INDEXOF_BLANK);
                UpdateMapEx(10, 3, 2, INDEXOF_BLANK);
                UpdateMapEx(10, 3, 3, INDEXOF_BLANK);
                UpdateMapEx(10, 4, 2, INDEXOF_BLANK);
                UpdateMapEx(10, 3, 9, INDEXOF_BLANK);
                UpdateMapEx(10, 3, 10, INDEXOF_BLANK);
                UpdateMapEx(10, 3, 11, INDEXOF_BLANK);
                UpdateMapEx(10, 4, 10, INDEXOF_BLANK);
                UpdateMapEx(10, 1, 6, INDEXOF_SKELETON + 2);
                UpdateMapEx(10, 4, 5, INDEXOF_SKELETON);
                UpdateMapEx(10, 4, 7, INDEXOF_SKELETON);
                UpdateMapEx(10, 5, 5, INDEXOF_SKELETON);
                UpdateMapEx(10, 5, 7, INDEXOF_SKELETON);
                UpdateMapEx(10, 6, 5, INDEXOF_SKELETON);
                UpdateMapEx(10, 6, 7, INDEXOF_SKELETON);
                UpdateMapEx(10, 4, 6, INDEXOF_SKELETON + 1);
                UpdateMapEx(10, 6, 6, INDEXOF_SKELETON + 1);
                UpdateMapEx(10, 3, 6, INDEXOF_IRON_DOOR);
                UpdateMapEx(10, 7, 6, INDEXOF_IRON_DOOR);
            }
        }
        void L10_R4_C6()// 骷髅队长
        {
            if (PointDetect(4, 6))
                return;
            if (game.map[10, 4, 5] == INDEXOF_BLANK &&
                game.map[10, 4, 6] == INDEXOF_BLANK &&
                game.map[10, 4, 7] == INDEXOF_BLANK &&
                game.map[10, 5, 5] == INDEXOF_BLANK &&
                game.map[10, 5, 7] == INDEXOF_BLANK &&
                game.map[10, 6, 5] == INDEXOF_BLANK &&
                game.map[10, 6, 6] == INDEXOF_BLANK &&
                game.map[10, 6, 7] == INDEXOF_BLANK)
            {
                UpdateMapEx(10, 3, 6, INDEXOF_BLANK);
            }
        }
        void L10_R2_C6()// 骷髅队长
        {
            if (PointDetect(2, 6))
                return;
            if (game.map[10, 1, 6] == INDEXOF_SKELETON + 2)
            {
                GeneralTalkDialog(dialog7);
            }
        }
        void L10_R1_C6()// 骷髅队长
        {
            if (PointDetect(1, 6))
                return;
            if (game.map[10, 1, 6] == INDEXOF_BLANK &&
                game.map[10, 7, 6] == INDEXOF_IRON_DOOR)
            {
                GeneralTalkDialog(dialog8);
                UpdateMapEx(10, 7, 6, INDEXOF_BLANK);
                UpdateMapEx(10, 4, 4, INDEXOF_BLANK);
                UpdateMapEx(10, 4, 8, INDEXOF_BLANK);
                UpdateMapEx(10, 11, 6, INDEXOF_UPSTAIR);
                UpdateMapEx(10, 2, 1, INDEXOF_BLANK);
                UpdateMapEx(10, 2, 11, INDEXOF_BLANK);
                UpdateMapEx(10, 2, 2, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(10, 2, 3, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(10, 2, 4, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(10, 2, 8, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(10, 2, 9, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(10, 2, 10, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(10, 3, 3, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(10, 3, 5, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(10, 3, 4, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(10, 3, 7, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(10, 3, 8, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(10, 3, 9, INDEXOF_RED_DRAG + 5);
            }
        }
        void L11_R5_C2()// 高级法师
        {
            GeneralDefendEvent(new POINT(5, 2), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L12_R1_C1()// 商人
        {
            GeneralTradeEvent(new POINT(1, 1), 800, 1, 0, 0);
        }
        void L12_R1_C11()// 商人
        {
            if (SpecialTradeEvent(1, game.braver.gold, 150))
            {
                game.YellowKeyCount++;
                game.braver.gold -= 150;
                UpdateBraverProperty();
                UpdateKeysView();
            }
        }
        void L14_R3_C2()//兽人武士
        {
            if (PointDetect(3, 2))
                return;
            if (game.map[14, 3, 1] == INDEXOF_BLANK + 1 &&
                game.map[14, 2, 2] == INDEXOF_BLANK &&
                game.map[14, 1, 1] == INDEXOF_BLANK &&
                game.map[14, 1, 3] == INDEXOF_BLANK)
            {
                UpdateMapEx(14, 3, 1, INDEXOF_RED_DRAG + 4);
            }
        }
        void L15_R6_C6()// 大章鱼
        {
            if (PointDetect(6, 6))
                return;
            if (game.map[15, 6, 6] == INDEXOF_BLANK &&
                game.map[15, 5, 6] != INDEXOF_BLANK)
            {
                UpdateMapEx(15, 6, 5, INDEXOF_BLANK);
                UpdateMapEx(15, 6, 7, INDEXOF_BLANK);
                UpdateMapEx(15, 5, 5, INDEXOF_BLANK);
                UpdateMapEx(15, 5, 7, INDEXOF_BLANK);
                UpdateMapEx(15, 4, 5, INDEXOF_BLANK);
                UpdateMapEx(15, 4, 6, INDEXOF_BLANK);
                UpdateMapEx(15, 4, 7, INDEXOF_BLANK);
                UpdateMapEx(15, 3, 6, INDEXOF_BLANK);
                UpdateMapEx(15, 5, 6, Array.IndexOf(items, "镐"));
            }
        }
        void L15_R11_C11()//商人
        {
            GeneralTradeEvent(new POINT(11, 11), 200, 0, 1, 0);
        }
        void L15_R1_C9()// 小偷
        {
            GeneralTalkDialog(dialog9);
            UpdateMapEx(15, 1, 8, INDEXOF_BLANK);
            UpdateMapEx(15, 1, 9, INDEXOF_BLANK);
            UpdateMapEx(29, 2, 6, INDEXOF_THIEF);
        }
        void L17_R8_C2()// 初级士兵
        {
            GeneralDefendEvent(new POINT(8, 2), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L17_R5_C2()// 初级士兵
        {
            GeneralDefendEvent(new POINT(5, 2), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L17_R8_C10()// 兽人
        {
            GeneralDefendEvent(new POINT(8, 10), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L17_R5_C10()// 兽人武士
        {
            GeneralDefendEvent(new POINT(5, 10), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L20_R8_C6()// 吸血鬼
        {
            if (PointDetect(8, 6))
                return;
            if (PointDetectEx(3, 6, INDEXOF_IRON_DOOR))
            {
                GeneralTalkDialog(dialog10);
                UpdateMapEx(20, 5, 5, INDEXOF_BLANK);
                UpdateMapEx(20, 5, 6, INDEXOF_BLANK);
                UpdateMapEx(20, 5, 7, INDEXOF_BLANK);
                UpdateMapEx(20, 6, 5, INDEXOF_BLANK);
                UpdateMapEx(20, 6, 7, INDEXOF_BLANK);
                UpdateMapEx(20, 7, 5, INDEXOF_BLANK);
                UpdateMapEx(20, 7, 6, INDEXOF_BLANK);
                UpdateMapEx(20, 7, 7, INDEXOF_BLANK);
                UpdateMapEx(20, 6, 6, Array.IndexOf(items, "吸血鬼"));
            }
        }
        void L20_R6_C6()// 吸血鬼
        {
            if (PointDetect(6, 6))
                return;
            if (PointDetectEx(3, 6, INDEXOF_IRON_DOOR) &&
                PointDetectEx(6, 6, INDEXOF_BLANK))
            {
                GeneralTalkDialog(dialog11);
                UpdateMapEx(20, 3, 6, INDEXOF_BLANK);
                UpdateMapEx(20, 4, 5, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(20, 4, 6, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(20, 4, 7, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(20, 8, 5, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(20, 8, 6, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(20, 8, 7, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(20, 5, 4, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(20, 6, 4, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(20, 7, 4, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(20, 5, 8, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(20, 6, 8, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(20, 7, 8, INDEXOF_RED_DRAG + 5);
            }
        }
        void L24_R1_C6()// 前往50层
        {
            if (PointDetect(1, 6))
                return;
            if (PointDetectEx(2, 6, INDEXOF_BLANK))
            {
                game.layer = 50;
                game.row = 7;
                game.col = 6;
                UpdateFloor();
            }
        }
        void L25_R6_C6()// 大法师
        {
            if (PointDetect(6, 6))
                return;
            if (PointDetectEx(5, 5, INDEXOF_BLANK + 1))
            {
                UpdateMapEx(25, 5, 5, INDEXOF_BLANK);
                UpdateMapEx(25, 5, 7, INDEXOF_BLANK);
                UpdateMapEx(25, 7, 5, INDEXOF_BLANK);
                UpdateMapEx(25, 7, 7, INDEXOF_BLANK);
                UpdateMapEx(25, 8, 4, INDEXOF_RED_DRAG + 4);
                UpdateMapEx(25, 8, 5, INDEXOF_RED_DRAG + 4);
                UpdateMapEx(25, 8, 7, INDEXOF_RED_DRAG + 4);
                UpdateMapEx(25, 8, 8, INDEXOF_RED_DRAG + 4);
            }
        }
        void L26_R6_C6()//公主
        {
            GeneralTalkDialog(dialog12);
            if (game.map[24, 1, 5] == INDEXOF_BLANK)
            {
                UpdateMapEx(24, 1, 5, INDEXOF_BLANK + 1);
                UpdateMapEx(24, 1, 7, INDEXOF_BLANK + 1);
                UpdateMapEx(24, 2, 6, INDEXOF_BLANK);
                UpdateMapEx(24, 3, 6, INDEXOF_BLANK);
                UpdateMapEx(24, 4, 6, INDEXOF_BLANK);
            }
        }
        void L28_R4_C8()// 商人
        {
            if (game.YellowKeyCount > 0)
            {
                if (SpecialTradeEvent(2, 1, 0))
                {
                    game.braver.gold += 100;
                    game.YellowKeyCount--;
                    UpdateBraverProperty();
                    UpdateKeysView();
                }
            }
            else
                Tips("你没有可出售的黄钥匙！");
        }
        void L29_R2_C6()// 小偷
        {
            GeneralTalkDialog(dialog13);
            UpdateMapEx(29, 2, 6, INDEXOF_BLANK);
            UpdateMapEx(29, 3, 6, INDEXOF_BLANK);
            UpdateMapEx(2, 7, 3, INDEXOF_THIEF);
            UpdateMapEx(2, 7, 2, INDEXOF_BLANK + 1);
        }
        void L30_R5_C6()// 史莱姆
        {
            GeneralDefendEvent(new POINT(5, 6),
                new POINT[] { new POINT(5, 3), new POINT(5, 4), new POINT(5, 5), new POINT(5, 7), new POINT(5, 8), new POINT(5, 9) },
                new POINT[] { new POINT(4, 6) });
        }
        void L32_R10_C6()// 骑士队长
        {
            if (PointDetect(10, 6))
                return;
            if (PointDetectEx(11, 7, INDEXOF_BLANK + 1))
            {
                GeneralTalkDialog(dialog14);
                UpdateMapEx(32, 11, 7, INDEXOF_BLANK);
                UpdateMapEx(40, 1, 6, INDEXOF_KNIGHT_LEADER);
            }
        }
        void L32_R9_C6()// 骑士队长
        {
            if (PointDetect(9, 6))
                return;
            if (game.map[40, 1, 6] == INDEXOF_KNIGHT_LEADER)
                GeneralTalkDialog(dialog15);
        }
        void L32_R10_C2()// 中级士兵
        {
            GeneralDefendEvent(new POINT(10, 2), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L33_R5_C10()// 骑士剑
        {
            if (PointDetect(5, 10))
                return;
            if (game.map[33, 5, 9] != INDEXOF_BLANK &&
                game.map[33, 5, 11] != INDEXOF_BLANK &&
                game.map[33, 7, 9] != INDEXOF_BLANK &&
                game.map[33, 7, 11] != INDEXOF_BLANK
                )
            {
                UpdateMapEx(33, 4, 10, INDEXOF_IRON_DOOR);
                UpdateMapEx(33, 8, 10, INDEXOF_IRON_DOOR);
            }
        }
        void L33_R7_C10()// 骑士剑
        {
            GeneralDefendEvent(new POINT(7, 10),
                new POINT[] { new POINT(5, 9), new POINT(5, 11), new POINT(7, 9), new POINT(7, 11) },
                new POINT[] { new POINT(4, 10), new POINT(8, 10) });
        }
        void L34_R6_C4()// 怪物
        {
            if (game.map[34, 6, 2] == INDEXOF_BLANK + 1 &&
                game.map[34, 4, 5] == INDEXOF_BLANK &&
                game.map[34, 4, 7] == INDEXOF_BLANK &&
                game.map[34, 4, 9] == INDEXOF_BLANK &&
                game.map[34, 4, 11] == INDEXOF_BLANK &&
                game.map[34, 8, 5] == INDEXOF_BLANK &&
                game.map[34, 8, 7] == INDEXOF_BLANK &&
                game.map[34, 8, 9] == INDEXOF_BLANK &&
                game.map[34, 8, 11] == INDEXOF_BLANK
                )
            {
                UpdateMapEx(34, 5, 1, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(34, 5, 3, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(34, 7, 1, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(34, 7, 3, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(34, 6, 2, INDEXOF_RED_DRAG + 4);
            }
        }
        void L35_R9_C5()// 小偷
        {
            GeneralTalkDialog(dialog17);
            UpdateMapEx(35, 9, 5, INDEXOF_BLANK);
            UpdateMapEx(35, 9, 4, INDEXOF_BLANK);
            UpdateMapEx(50, 5, 6, INDEXOF_THIEF);
        }
        void L35_R7_C6()// 魔龙
        {
            if (PointDetect(7, 6))
                return;
            if (game.map[35, 7, 6] == INDEXOF_BLANK &&
                game.map[35, 6, 6] != INDEXOF_BLANK)
            {
                UpdateMapEx(35, 7, 5, INDEXOF_BLANK);
                UpdateMapEx(35, 7, 7, INDEXOF_BLANK);
                UpdateMapEx(35, 6, 5, INDEXOF_BLANK);
                UpdateMapEx(35, 6, 7, INDEXOF_BLANK);
                UpdateMapEx(35, 5, 5, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(35, 5, 6, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(35, 5, 7, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(35, 3, 6, INDEXOF_BLANK);
                UpdateMapEx(35, 6, 6, Array.IndexOf(items, "冰冻魔法"));
            }
        }
        void L38_R2_C5()// 商人
        {
            GeneralTradeEvent(new POINT(2, 5), 200, 0, 0, 3);
        }
        void L38_R10_C2()// 中级士兵
        {
            GeneralDefendEvent(new POINT(10, 2), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L39_R2_C9()// 商人
        {
            GeneralTradeEvent(new POINT(2, 9), 1000, 0, 1, 5);
        }
        void L39_R4_C4()// 对称飞
        {
            if (game.props["对称飞"] == 0 &&
                PointDetectEx(2, 4, INDEXOF_BLANK) &&
                PointDetectEx(4, 4, INDEXOF_BLANK) &&
                PointDetectEx(4, 6, INDEXOF_BLANK)
                )
            {
                game.nr = 3;
                game.nc = 4;
                UpdateBraverLocation();
                UpdateMapEx(39, 4, 4, Array.IndexOf(items, "对称飞"));
            }
        }
        void L40_R2_C6()// 骑士队长
        {
            if (PointDetect(2, 6))
                return;
            if (PointDetectEx(1, 6, INDEXOF_KNIGHT_LEADER) &&
                PointDetectEx(2, 4, INDEXOF_BLANK) &&
                PointDetectEx(2, 8, INDEXOF_BLANK))
            {
                GeneralTalkDialog(dialog18);
            }
        }
        void L40_R1_C6()// 骑士队长
        {
            if (PointDetect(1, 6))
                return;
            if (!PointDetectEx(1, 6, INDEXOF_UPSTAIR))
            {
                GeneralTalkDialog(dialog19);
                UpdateMapEx(40, 2, 2, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(40, 2, 3, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(40, 2, 4, INDEXOF_RED_DRAG + 1);
                UpdateMapEx(40, 2, 8, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(40, 2, 9, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(40, 2, 10, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(40, 4, 3, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(40, 4, 4, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(40, 4, 5, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(40, 4, 7, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(40, 4, 8, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(40, 4, 9, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(42, 10, 6, INDEXOF_KNIGHT_LEADER);
                game.nr = 2;
                game.nc = 6;
                UpdateBraverLocation();
                UpdateMapEx(40, 1, 6, INDEXOF_UPSTAIR);
            }
        }
        void L41_R2_C6()// 下层飞
        {
            if (PointDetect(2, 6))
                return;
            if (game.props["下层飞"] == 0 &&
                PointDetectEx(2, 2, INDEXOF_BLANK) &&
                PointDetectEx(2, 10, INDEXOF_BLANK)
                )
            {
                UpdateMapEx(41, 4, 6, Array.IndexOf(items, "下层飞"));
                UpdateMapEx(41, 5, 6, INDEXOF_BLANK + 1);
            }
        }
        void L42_R10_C5()// 骑士队长
        {
            if (PointDetect(10, 5))
                return;
            if (PointDetectEx(10, 6, INDEXOF_KNIGHT_LEADER))
            {
                UpdateMapEx(42, 10, 6, INDEXOF_BLANK);
                UpdateMapEx(42, 8, 6, INDEXOF_KNIGHT_LEADER);
                UpdateMap(6, 6, "魔王");
                UpdateMap(7, 6, "魔法警卫");
                UpdateMap(9, 6, "魔法警卫");
                UpdateMap(8, 5, "魔法警卫");
                UpdateMap(8, 7, "魔法警卫");
                GeneralTalkDialog(dialog20);
                UpdateMap(6, 6, "空白");
                UpdateMap(7, 6, "空白");
                UpdateMap(9, 6, "空白");
                UpdateMap(8, 5, "空白");
                UpdateMap(8, 7, "空白");
                UpdateMapEx(42, 8, 6, INDEXOF_BLANK);
            }
        }
        void L44_R9_C6()// 高级士兵
        {
            GeneralDefendEvent(new POINT(9, 6), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L45_R3_C9()// 商人
        {
            if (SpecialTradeEvent(3, game.braver.gold, 1000))
            {
                game.braver.gold -= 1000;
                game.braver.life += 2000;
                UpdateBraverProperty();
                UpdateMapEx(45, 3, 9, INDEXOF_BLANK);
            }
        }
        void L45_R10_C8()// 魔法警卫
        {
            GeneralDefendEvent(new POINT(10, 8), DIRECTION.VERTICAL, DIRECTION.LEFT);
        }
        void L45_R10_C5()// 黑暗骑士
        {
            GeneralDefendEvent(new POINT(10, 5), DIRECTION.VERTICAL, DIRECTION.LEFT);
        }
        void L47_R2_C5()// 商人
        {
            if (SpecialTradeEvent(4, game.braver.gold, 4000))
            {
                game.braver.gold -= 4000;
                UpdateBraverProperty();
                GetProp("地震卷轴");
                UpdateMapEx(47, 2, 5, INDEXOF_BLANK);
            }
        }
        void L49_R10_C6()// 高级巫师
        {
            GeneralDefendEvent(new POINT(10, 6), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L49_R8_C6()// 黑暗骑士
        {
            GeneralDefendEvent(new POINT(8, 6), DIRECTION.HORIZONTAL, DIRECTION.UP);
        }
        void L49_R3_C6()// 假魔王
        {
            if (PointDetect(3, 6))
                return;
            if (//game.props["屠龙匕首"] == 0 &&
                PointDetectEx(5, 2, INDEXOF_BLANK + 1)
                )
            {
                UpdateMapEx(49, 2, 5, INDEXOF_RED_DRAG + 4);
                UpdateMapEx(49, 2, 7, Array.IndexOf(items, "屠龙匕首"));
                UpdateMapEx(49, 4, 2, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(49, 4, 3, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(49, 4, 4, INDEXOF_RED_DRAG + 2);
                UpdateMapEx(49, 4, 8, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(49, 4, 9, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(49, 4, 10, INDEXOF_RED_DRAG + 3);
                UpdateMapEx(49, 5, 5, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(49, 5, 6, INDEXOF_RED_DRAG + 5);
                UpdateMapEx(49, 5, 7, INDEXOF_RED_DRAG + 5);

                UpdateMapEx(49, 5, 2, INDEXOF_BLANK);
                UpdateMapEx(49, 6, 2, INDEXOF_BLANK);
                UpdateMapEx(49, 7, 2, INDEXOF_BLANK);
                UpdateMapEx(49, 8, 2, INDEXOF_BLANK);
                UpdateMapEx(49, 5, 10, INDEXOF_BLANK);
                UpdateMapEx(49, 6, 10, INDEXOF_BLANK);
                UpdateMapEx(49, 7, 10, INDEXOF_BLANK);
                UpdateMapEx(49, 8, 10, INDEXOF_BLANK);
            }
        }
        void L50_R6_C6()// 魔王
        {
            if (PointDetect(6, 6))
                return;
            GeneralTalkDialog(dialog21);
            UpdateMapEx(50, 5, 6, Array.IndexOf(items, "魔王"));
        }
        void L50_R5_C6()// 魔王
        {
            if (PointDetect(5, 6))
                return;
            GeneralTalkDialog(dialog22);
            GameInit();
        }

        void EventDetect()
        {
            switch (game.layer)
            {
                case 2: L2_R2_C6(); break;
                case 3: L3_R9_C5(); break;
                case 8: L8_R5_C10(); break;
                case 10:
                    L10_R1_C6();
                    L10_R2_C6();
                    L10_R4_C6();
                    L10_R5_C6();
                    break;
                case 11: L11_R5_C2(); break;
                case 14: L14_R3_C2(); break;
                case 15: L15_R6_C6(); break;
                case 17:
                    L17_R8_C2();
                    L17_R5_C2();
                    L17_R8_C10();
                    L17_R5_C10();
                    break;
                case 20:
                    L20_R8_C6();
                    L20_R6_C6();
                    break;
                case 24: L24_R1_C6(); break;
                case 25: L25_R6_C6(); break;
                case 30: L30_R5_C6(); break;
                case 32:
                    L32_R10_C6();
                    L32_R9_C6();
                    L32_R10_C2();
                    break;
                case 33:
                    L33_R5_C10();
                    L33_R7_C10();
                    break;
                case 34: L34_R6_C4(); break;
                case 35: L35_R7_C6(); break;
                case 38: L38_R10_C2(); break;
                case 39: L39_R4_C4(); break;
                case 40:
                    L40_R1_C6();
                    L40_R2_C6();
                    break;
                case 41: L41_R2_C6(); break;
                case 42: L42_R10_C5(); break;
                case 44: L44_R9_C6(); break;
                case 45:
                    L45_R10_C5();
                    L45_R10_C8();
                    break;
                case 49:
                    L49_R3_C6();
                    L49_R8_C6();
                    L49_R10_C6();
                    break;
                case 50:
                    L50_R5_C6();
                    L50_R6_C6();
                    break;
                default: break;
            }
        }
    }
}
