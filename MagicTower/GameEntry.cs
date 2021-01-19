using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class GameEntry : Form
    {
        bool debug = false;
        public GameEntry()
        {
            KeyPreview = true;
            InitializeComponent();
            GUI_Init();
            GameInit();
        }
        void GameInit()
        {
            int i = 0, j = 0, k = 0;
            settings.Reload();
            root = Directory.GetCurrentDirectory() + "\\魔塔\\";
            if (Directory.Exists(root))
            {
                Tips("读取资源文件！(1/4)");
                files.AddRange(Directory.GetFiles(root + "怪物\\"));
                files.AddRange(Directory.GetFiles(root + "物品\\"));
                files.AddRange(Directory.GetFiles(root + "其它\\"));
                imgs = new Dictionary<string, Image>();
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    int ei = fi.Name.LastIndexOf('.');
                    imgs[fi.Name.Substring(0, ei)] = Image.FromFile(file);
                }
                game.map = new byte[MAX_LAYER, MAX_ROW, MAX_COL];
                game.props = new Dictionary<string, short>();
                foreach (string s in PropList)
                    game.props[s] = 0;
                game.braver.name = "勇者4";
                game.monster.name = null;
                game.layer = 1;
                game.row = 11;
                game.col = 6;
                game.dir = DIRECTION.UP;
                game.ShoppingTime = 1;

                if (!debug)
                {
                    game.braver.life = 1000;
                    game.braver.attack = 100;
                    game.braver.defense = 100;
                    game.braver.gold = 0;
                    game.YellowKeyCount = 0;
                    game.BlueKeyCount = 0;
                    game.RedKeyCount = 0;
                }
                else
                {
                    game.braver.life = 1000;
                    game.braver.attack = 1000;
                    game.braver.defense = 1000;
                    game.braver.gold = 10000;
                    game.YellowKeyCount = 100;
                    game.BlueKeyCount = 100;
                    game.RedKeyCount = 100;
                    game.mlayer = 50;
                    foreach (string s in PropList)
                        GetProp(s);
                }

                if (settings.items == "")
                {
                    Tips("读取游戏数据！(2/4)");
                    excel1 = new MyControl.ExcelAccess(MyControl.Direction.READ, false);
                    excel1.Open(root + "游戏数据.xlsx");
                    excel1.Seek(1, 1, 1);
                    excel1.NextLine();
                    for (i = 0; i < MAX_MONSTERS; i++)
                    {
                        for (j = 0; j < 5; j++)
                        {
                            tmp = excel1.Read();
                            settings.entities += tmp + depart;
                        }
                        excel1.NextLine();
                    }
                    excel1.AutoWrap = true;
                    excel1.NextSheet();
                    for (i = 0; i < MAX_ITEMS; i++)
                    {
                        tmp = excel1.Read();
                        settings.items += tmp + depart;
                    }
                    excel1.AutoWrap = false;
                    excel1.Seek(2, 2, 3);
                    for (i = 0; i < MAX_LAYER; i++)
                    {
                        for (j = 0; j < 6; j++)
                        {
                            tmp = excel1.Read();
                            settings.jumptable += tmp + depart;
                        }
                        excel1.NextLine();
                        excel1.NextCell();
                    }
                    excel1.Close();
                    Tips("读取地图数据！(3/4)");
                    excel1.Open(root + "地图数据.xlsx");
                    for (i = 0; i < MAX_LAYER; i++)
                    {
                        for (j = 0; j < MAX_ROW; j++)
                        {
                            for (k = 0; k < MAX_COL; k++)
                            {
                                tmp = excel1.Read();
                                settings.map += tmp + depart;
                            }
                            excel1.NextLine();
                        }
                        excel1.NextSheet();
                    }
                    excel1.Close();
                    settings.Save();
                }

                Tips("游戏显示初始化！(4/4)");
                MyControl.TxtAccess txt1 = new MyControl.TxtAccess(MyControl.Direction.READ, System.Text.Encoding.UTF8, ';');
                txt1.SetString(settings.entities);
                for (i = 0; i < MAX_MONSTERS; i++)
                {
                    entities[i].name = txt1.Read();
                    MonsterList[i] = entities[i].name;
                    int.TryParse(txt1.Read(), out entities[i].life);
                    int.TryParse(txt1.Read(), out entities[i].attack);
                    int.TryParse(txt1.Read(), out entities[i].defense);
                    int.TryParse(txt1.Read(), out entities[i].gold);
                }
                txt1.SetString(settings.items);
                for (i = 0; i < MAX_ITEMS; i++)
                {
                    tmp = txt1.Read();
                    items[i] = tmp;
                }
                txt1.SetString(settings.jumptable);
                for (i = 0; i < MAX_LAYER; i++)
                {
                    byte.TryParse(txt1.Read(), out JumpTable[i].下行楼层);
                    byte.TryParse(txt1.Read(), out JumpTable[i].下行行号);
                    byte.TryParse(txt1.Read(), out JumpTable[i].下行列号);
                    byte.TryParse(txt1.Read(), out JumpTable[i].上行楼层);
                    byte.TryParse(txt1.Read(), out JumpTable[i].上行行号);
                    byte.TryParse(txt1.Read(), out JumpTable[i].上行列号);
                }
                txt1.SetString(settings.map);
                for (i = 0; i < MAX_LAYER; i++)
                {
                    for (j = 0; j < MAX_ROW; j++)
                    {
                        for (k = 0; k < MAX_COL; k++)
                        {
                            tmp = txt1.Read();
                            if (tmp == "")
                                game.map[i, j, k] = 0;
                            else
                            {
                                int v = Array.IndexOf(items, tmp);
                                game.map[i, j, k] = (byte)v;
                            }
                        }
                    }
                }

                UpdateFloor();
                UpdateMap(game.row, game.col, game.braver.name);
                UpdateBraverProperty();
                UpdateMonsterProperty();
                UpdateKeysView();
                UpdateText(0, "武器");
                UpdateText(1, "防具");
                UpdateText(8, "神圣剑");
                UpdateText(9, "神圣盾");
                UpdatePictureBox(PICTUREBOX_OFFSET, "心");
                UpdatePictureBox(PICTUREBOX_OFFSET + 1, "铁剑");
                UpdatePictureBox(PICTUREBOX_OFFSET + 2, "铁盾");
                UpdatePictureBox(PICTUREBOX_OFFSET + 3, "幸运金币");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 5, "神圣剑");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 4, "神圣盾");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 3, "黄钥匙");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 2, "蓝钥匙");
                UpdatePictureBox(MONSTER_ICON_OFFSET - 1, "红钥匙");
                UpdatePictureBox(MONSTER_ICON_OFFSET + 1, "心");
                UpdatePictureBox(MONSTER_ICON_OFFSET + 2, "铁剑");
                UpdatePictureBox(MONSTER_ICON_OFFSET + 3, "铁盾");
                UpdatePictureBox(MONSTER_ICON_OFFSET + 4, "幸运金币");
                Tips("游戏显示初始化完毕！(4/4)");
            }
            else
                Tips("找不到\"魔塔\"文件夹!");
        }
        void GUI_Init()
        {
            int i = 0;
            SuspendLayout();

            main = new MyControl.VBox();
            main.AddHBox(0, 0);
            main.AddPanel(0, 0);
            main.SetVertical(new short[] { -1, 40 });
            main.SetID(1, id2++);

            MyControl.Panel p = main.children[0];
            p.AddVBox(10, 20);
            p.AddVBox(0, 0);
            p.AddVBox(10, 20);
            p.SetHorizontal(new short[] { 230, -1, 230 });

            MyControl.Panel p1 = p.children[0];
            p1.AddEmpty(0, 0);
            p1.AddPanel(0, 0);
            p1.AddGrid(0, 5);
            p1.AddGrid(0, 5);
            p1.SetVertical(new short[] { -1, 32, 150, -5 });
            p1.SetID(1, id2++);

            p1 = p.children[1];
            p1.AddEmpty(0, 0);
            p1.AddGrid(0, 0);
            p1.AddEmpty(0, 0);
            p1.SetVertical(new short[] { 103, -1, 103 });

            p1 = p.children[2];
            p1.AddEmpty(0, 0);
            p1.AddHBox(0, 0);
            p1.AddHBox(0, 0);
            p1.AddGrid(0, 0);
            p1.AddVBox(0, 0);
            p1.AddEmpty(0, 0);
            p1.SetVertical(new short[] { -1, 64, 64, 128, 200, -1 });

            p1 = p.children[0].children[2];
            p1.AddPanel(0, 0, 8);
            p1.SetHorizontal(new short[] { 32, -1 });
            p1.SetVertical(p1.GetDefault(4));
            p1.SetID(0, id3++);
            p1.SetID(2, id3++);
            p1.SetID(4, id3++);
            p1.SetID(6, id3++);
            p1.SetID(1, id2++);
            p1.SetID(3, id2++);
            p1.SetID(5, id2++);
            p1.SetID(7, id2++);

            p1 = p.children[0].children[3];
            p1.AddPanel(0, 0, 15);
            p1.SetHorizontal(p1.GetDefault(3));
            p1.SetVertical(p1.GetDefault(5));
            for (i = 0; i < 15; i++)
                p1.SetID((byte)i, id3++);

            p1 = p.children[1].children[1];
            p1.AddPanel(0, 0, 169);
            p1.SetHorizontal(p1.GetDefault(13));
            p1.SetVertical(p1.GetDefault(13));
            for (i = 0; i < 169; i++)
                p1.SetID((byte)i, id3++);

            p1 = p.children[2].children[1];
            p1.AddVBox(0, 0);
            p1.AddPanel(0, 0);
            p1.SetHorizontal(new short[] { -1, 64 });
            p1.SetID(1, id3++);

            p1 = p1.children[0];
            p1.AddPanel(0, 0);
            p1.AddPanel(0, 0);
            p1.SetVertical(p1.GetDefault(2));
            p1.SetID(0, id1++);
            p1.SetID(1, id2++);

            p1 = p.children[2].children[2];
            p1.AddVBox(0, 0);
            p1.AddPanel(0, 0);
            p1.SetHorizontal(new short[] { -1, 64 });
            p1.SetID(1, id3++);

            p1 = p1.children[0];
            p1.AddPanel(0, 0);
            p1.AddPanel(0, 0);
            p1.SetVertical(p1.GetDefault(2));
            p1.SetID(0, id1++);
            p1.SetID(1, id2++);

            p1 = p.children[2].children[3];
            p1.AddPanel(0, 0, 6);
            p1.SetHorizontal(new short[] { 32, -1 });
            p1.SetVertical(p1.GetDefault(3));
            p1.SetID(0, id3++);
            p1.SetID(2, id3++);
            p1.SetID(4, id3++);
            p1.SetID(1, id2++);
            p1.SetID(3, id2++);
            p1.SetID(5, id2++);

            p1 = p.children[2].children[4];
            p1.AddHBox(0, 0);
            p1.AddPanel(0, 0);
            p1.AddGrid(0, 0);
            p1.SetVertical(new short[] { 80, -1, 96 });
            p1.SetID(1, id2++);

            MyControl.Panel p2 = p1.children[0];
            p2.AddEmpty(0, 0);
            p2.AddPanel(0, 0);
            p2.AddEmpty(0, 0);
            p2.SetHorizontal(new short[] { -1, 80, -1 });
            p2.SetID(1, id3++);

            p2 = p1.children[2];
            p2.AddPanel(0, 0, 8);
            p2.SetHorizontal(new short[] { 32, -1 });
            p2.SetVertical(p2.GetDefault(4));
            p2.SetID(0, id3++);
            p2.SetID(2, id3++);
            p2.SetID(4, id3++);
            p2.SetID(6, id3++);
            p2.SetID(1, id2++);
            p2.SetID(3, id2++);
            p2.SetID(5, id2++);
            p2.SetID(7, id2++);

            Controls.AddRange(MyControl.Panel.GetLabels(2));
            Controls.AddRange(MyControl.Panel.GetTextBoxes(16));
            for (i = 0; i < 198; i++)
            {
                pics[i] = new PictureBox();
                pics[i].Visible = true;
                pics[i].SizeMode = PictureBoxSizeMode.Zoom;
            }
            Controls.AddRange(pics);

            ResumeLayout(false);

            MyControl.Panel.output = new List<MyControl.Panel.PARAM>();
            Resize += Form1_Resize;
            KeyDown += Form1_KeyDown;
            Form1_Resize(null, null);

            for (i = PICTUREBOX_OFFSET + 4; i < PICTUREBOX_OFFSET + 19; i++)
                Controls[i].Click += PropCallback;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                case Keys.H:
                    talk_dialog.Init(HelpContent);
                    talk_dialog.ShowDialog();
                    break;
                case Keys.A:
                    GameInit();
                    break;
                case Keys.R:
                    settings.Reset();
                    GameInit();
                    break;
                case Keys.S:
                    Tips("游戏已保存！");
                    game_bak = game;
                    game_bak.map = (byte[,,])game.map.Clone();
                    game_bak.props = new Dictionary<string, short>(game.props);
                    break;
                case Keys.L:
                    Tips("游戏已载入！");
                    game = game_bak;
                    game.map = (byte[,,])game_bak.map.Clone();
                    game.props = new Dictionary<string, short>(game_bak.props);

                    for (int i = 0; i < PropList.Length; i++)
                    {
                        tmp = PropList[i];
                        if (game.props[tmp] != 0)
                            UpdatePropsView(i, tmp);
                        else
                            UpdatePropsView(i, "空白");
                    }
                    UpdateFloor();
                    UpdateKeysView();
                    break;
                case Keys.Subtract:
                    if (debug)
                    {
                        if (--tmp_layer > MAX_LAYER)
                            tmp_layer = MAX_LAYER - 1;
                        DisplayMap(tmp_layer);
                    }
                    break;
                case Keys.Add:
                    if (debug)
                    {
                        if (++tmp_layer > MAX_LAYER - 1)
                            tmp_layer = 0;
                        DisplayMap(tmp_layer);
                    }
                    break;
                case Keys.PageUp:
                    if (game.props["楼层飞"] == -1 &&
                        game.layer < game.mlayer
                        )
                    {
                        GoUpStair();
                    }
                    break;
                case Keys.PageDown:
                    if (game.props["楼层飞"] == -1 &&
                        game.layer > 1
                        )
                    {
                        GoDownStair();
                    }
                    break;
                case Keys.Left: BraverMove(DIRECTION.LEFT); break;
                case Keys.Right: BraverMove(DIRECTION.RIGHT); break;
                case Keys.Up: BraverMove(DIRECTION.UP); break;
                case Keys.Down: BraverMove(DIRECTION.DOWN); break;
                default:
                    break;
            }
        }

        void DisplayMap(int layer)
        {
            if (layer < 0 || layer > 50)
                return;
            for (int i = 0; i < MAX_ROW; i++)
            {
                for (int j = 0; j < MAX_COL; j++)
                {
                    tmp = items[game.map[layer, i, j]];
                    UpdateMap(i, j, tmp);
                }
            }
        }
        void Tips(string str)
        {
            Controls[2].Text = str;
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            main.rect.Width = Width - 20;
            main.rect.Height = Height - 50;
            main.Update();
            MyControl.Panel.output.Clear();
            main.Print();
            foreach (MyControl.Panel.PARAM x in MyControl.Panel.output)
            {
                Controls[x.id].Location = x.rect.Location;
                Controls[x.id].Size = x.rect.Size;
            }
        }

        struct ENTITY
        {
            public string name;
            public int life, attack, defense, gold;
        };
        struct JUMP
        {
            public byte 下行楼层, 下行行号, 下行列号;
            public byte 上行楼层, 上行行号, 上行列号;
        };
        struct POINT
        {
            public POINT(int row, int col)
            {
                r = row;
                c = col;
            }
            public int r, c;
        };
        enum DIRECTION { LEFT, RIGHT, UP, DOWN, HORIZONTAL, VERTICAL };
        struct GameData
        {
            public ENTITY braver, monster;
            public DIRECTION dir;
            public byte layer, row, col, mlayer, nr, nc;
            public byte ShoppingTime, YellowKeyCount, BlueKeyCount, RedKeyCount;
            public Dictionary<string, short> props;
            public byte[,,] map;
        };
        const byte MAX_LAYER = 51, MAX_ROW = 13, MAX_COL = 13, MAX_MONSTERS = 34, MAX_ITEMS = 105,
            LABEL_OFFSET = 0, TEXTBOX_OFFSET = 2, PICTUREBOX_OFFSET = 18, MONSTER_ICON_OFFSET = 211, MAP_OFFSET = 37;
        short id1 = LABEL_OFFSET, id2 = TEXTBOX_OFFSET, id3 = PICTUREBOX_OFFSET;

        char depart = ';';
        byte tmp_layer = 0;
        string tmp, root;
        POINT[] pts = new POINT[9];
        JUMP[] JumpTable = new JUMP[MAX_LAYER];
        ENTITY[] entities = new ENTITY[MAX_MONSTERS];
        string[] items = new string[MAX_ITEMS];
        PictureBox[] pics = new PictureBox[198];// 4+15+169+2+3+5
        string[] MonsterList = new string[34];
        string[] PropList =
        {
            "神圣盾", "幸运金币", "楼层飞", "怪物手册",
            "记事本","十字架", "冰冻魔法", "屠龙匕首",
            "镐", "圣水", "炸弹", "下层飞", "上层飞", "大黄钥匙", "地震卷轴",
            "对称飞",
        };
        string[] EquipList =
        {
            "铁剑","银剑","骑士剑","圣剑","神圣剑",
            "铁盾","银盾","骑士盾","圣盾","神圣盾"
        };
        string[] MaterialList =
        {
            "红药水","蓝药水","红宝石", "蓝宝石",
            "黄钥匙","蓝钥匙","红钥匙"
        };
        string[] UnreachableList =
        {
            "商店1","商店2","墙","铁门","岩浆",
            "大章鱼1","大章鱼2","大章鱼3","大章鱼4","大章鱼5","大章鱼6","大章鱼7","大章鱼8",
            "魔龙1","魔龙2","魔龙3","魔龙4","魔龙5","魔龙6","魔龙7","魔龙8",
        };
        string[] ActionList =
        {
            "上行楼梯","下行楼梯","商店",
            "黄门","红门","蓝门",
            "商人","智者","小偷","公主","魔王",
        };
        string[] HelpContent =
        {
            "操作说明：方向键移动，A重新开始游戏，S保存，L加载,R重新加载地图，H或F1弹出帮助窗口",
            "战斗说明：勇者先手，如果能秒杀怪物，不扣血；如果攻击低于怪物防御或经计算会死亡，不可攻击",
            "资源：红蓝宝石、红蓝药水、红黄蓝钥匙",
            "装备：铁/银/骑士/圣/神圣共5个等级的剑/盾，显示在右上角",
            "道具：分一次、多次、永久的道具，如果有正常显示图标，鼠标左键单击使用"
        };
        string[] Notepad =
        {
            "1：有些门不能用钥匙打开，只有当你打败它的守卫后才会自动打开。",
            "2：我听说在塔内有2把隐藏的红钥匙。",
            "3：在这区域不多次提升攻击力，就不能打败石头人。切记前人教训！",
            "4：你购买了礼物后再与商人对话，他会告诉你一些重要的消息。",
            "5：魔塔一共50层，每10层为一个区域。如果不打败此区域的头目就不能到更高的地方。",
            "6：大法师住在25楼，他是魔塔的主人。以你现在的状态去攻击他简直就是自杀。你应当在取得更高级别的道具后再去打败他。",
            "7：我没有什么可说的，但有一个确切的消息藏在这个楼层里。",
            "8：如果你到27楼时状态为：生命1500，攻击80，防御98，拥有1蓝钥匙，5黄钥匙。那么祝贺你，你前期是比较成功的。",
            "9：别匆忙，放慢速度。",
            "10：双手剑士的攻击力太高了，你最好到能对他一击必杀时再与他战斗。",
            "11：你需要用地震卷轴取出37楼仓库内的所有宝物。",
            "12：你是否注意到5，9，14，16，18楼有的墙与众不同？",
            "13：谜题：“在3点，拥有传送功能的密宝就会出现。”",
            "14：在商店里你最好选择提升防御，只有在攻击力低于敌人的防御力时才提升攻击力。",
            "15：如果你持有十字架，面对兽人和吸血鬼时你的攻击力加倍。在没有十字架的情况下你不可能打败吸血鬼。十字架被藏在高于15楼的墙内。",
            "16：如果要打败魔龙你需要圣剑、圣盾、屠龙匕或更高等级的装备。",
            "17：41楼事实上是左右对称的。",
            "18：巫师会用魔法攻击路过的人，在2个魔法警卫间通过会使你的生命减少一半。",
            "19：如果你能用好4种移动宝物，你不用与强敌作战就能上楼。",
            "20：44楼被藏在异空间，你只能用密宝才能到达。",
            "21：象骰子上5的形状是一种封印魔法，你最好记住它在你与49楼假魔王战斗时有用。",
            "22：魔塔有50层高，但似乎你不能直接到50楼。",
            "23：存放圣剑的房间的门坏了，你必须用镐破墙而入。",
            "24：神圣盾能防御魔法攻击，但它被藏在异空间的楼层内。",
            "25：塔内有个幸运金币。拥有它在打败敌人后能获得2倍的金钱。"
        };

        Shop shop_dialog = new Shop();
        Trade trade_dialog = new Trade();
        Talk talk_dialog = new Talk();
        GameData game = new GameData();
        GameData game_bak = new GameData();
        List<string> files = new List<string>();
        Properties.Settings settings = new Properties.Settings();
        MyControl.VBox main;
        Dictionary<string, Image> imgs;
        MyControl.ExcelAccess excel1;
    }
}
