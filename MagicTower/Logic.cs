using System;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class GameEntry : Form
    {
        void PropCallback(object sender, EventArgs e)
        {
            int ind = Controls.IndexOf((Control)sender), i, j;
            switch (ind - PICTUREBOX_OFFSET - 4)
            {
                case 1:// 楼层飞
                    Tips("PageUp 键上楼,PageDown 键下楼。");
                    break;
                case 2:// 怪物手册
                    tmp = "怪物名称\t\t生命\t攻击\t防御\t金币\t伤害\r\n";
                    for (i = 0; i < MAX_MONSTERS; i++)
                    {
                        game.monster = entities[i];
                        ind = CalculateDamage();
                        if (ind != 0)
                        {
                            tmp += game.monster.name + "\t";
                            if (game.monster.name.Length <= 4)
                                tmp += "\t";
                            tmp += game.monster.life.ToString() + "\t" +
                                game.monster.attack.ToString() + "\t" +
                                game.monster.defense.ToString() + "\t" +
                                game.monster.gold.ToString() + "\t";
                            if (ind == -1)
                                tmp += "无法攻击！";
                            else
                                tmp += ind.ToString();
                            tmp += "\r\n";
                        }
                    }
                    GeneralTalkDialog(new string[] { tmp });
                    game.monster.name = null;
                    break;
                case 3:// 记事本
                    GeneralTalkDialog(Notepad);
                    break;
                case 5:// 冰冻魔法
                    if (game.layer == 13 ||
                        game.layer == 26)
                    {
                        for (i = 1; i < 12; i++)
                        {
                            for (j = 1; j < 12; j++)
                            {
                                if (game.map[game.layer, i, j] == INDEXOF_BLANK + 2)
                                    UpdateMapEx(game.layer, i, j, INDEXOF_BLANK);
                            }
                        }
                    }
                    break;
                case 7:// 镐
                    if (game.props["镐"] == 1)
                    {
                        i = 3;
                        GetRound(game.row, game.col);
                        switch (game.dir)
                        {
                            case DIRECTION.LEFT: i = 3; break;
                            case DIRECTION.RIGHT: i = 5; break;
                            case DIRECTION.UP: i = 1; break;
                            case DIRECTION.DOWN: i = 7; break;
                            default: break;
                        }
                        if (PointDetectEx(pts[i].r, pts[i].c, INDEXOF_BLANK + 1))
                        {
                            UpdateMapEx(game.layer, pts[i].r, pts[i].c, INDEXOF_BLANK);
                            game.props["镐"] = 0;
                            UpdatePictureBox(ind, "空白");
                        }
                    }
                    break;
                case 8:// 圣水
                    if (game.props["圣水"] == 1)
                    {
                        game.props["圣水"] = 0;
                        game.braver.life += 10 * (game.braver.attack + game.braver.defense);
                        UpdateBraverProperty();
                        UpdatePictureBox(ind, "空白");
                    }
                    break;
                case 9:// 炸弹
                    if (game.props["炸弹"] == 1)
                    {
                        game.props["炸弹"] = 0;
                        GetRound(game.row, game.col);
                        for (i = 1; i < 8; i += 2)
                        {
                            tmp = items[game.map[game.layer, pts[i].r, pts[i].c]];
                            if (Array.IndexOf(MonsterList, tmp) != -1)
                                UpdateMapEx(game.layer, pts[i].r, pts[i].c, INDEXOF_BLANK);
                        }
                        UpdateBraverProperty();
                        UpdatePictureBox(ind, "空白");
                    }
                    break;
                case 10:// 下层飞
                    if (game.props["下层飞"] == 1)
                    {
                        game.props["下层飞"] = 0;
                        UpdatePictureBox(ind, "空白");
                        if (game.layer == 1 || game.layer == 45)
                        {
                            game.layer--;
                            game.row = 1;
                            game.col = 2;
                            UpdateFloor();
                        }
                        else if (game.layer != 50)
                        {
                            GoDownStair();
                        }
                    }
                    break;
                case 11:// 上层飞
                    if (game.props["上层飞"] == 1)
                    {
                        game.props["上层飞"] = 0;
                        UpdatePictureBox(ind, "空白");
                        if (game.layer == 0 || game.layer == 43)
                        {
                            game.layer++;
                            game.row = 1;
                            game.col = 2;
                            UpdateFloor();
                        }
                        else if (game.layer != 49)
                        {
                            GoUpStair();
                        }
                    }
                    break;
                case 12:// 大黄钥匙
                    if (game.props["大黄钥匙"] == 1)
                    {
                        game.props["大黄钥匙"] = 0;
                        UpdatePictureBox(ind, "空白");
                        for (i = 1; i < 12; i++)
                        {
                            for (j = 1; j < 12; j++)
                            {
                                if (game.map[game.layer, i, j] == INDEXOF_BLANK + 4)
                                    UpdateMapEx(game.layer, i, j, INDEXOF_BLANK);
                            }
                        }
                    }
                    break;
                case 13:// 地震卷轴
                    if (game.props["地震卷轴"] == 1)
                    {
                        game.props["地震卷轴"] = 0;
                        UpdatePictureBox(ind, "空白");
                        for (i = 1; i < 12; i++)
                        {
                            for (j = 1; j < 12; j++)
                            {
                                if (game.map[game.layer, i, j] == INDEXOF_BLANK + 1)
                                    UpdateMapEx(game.layer, i, j, INDEXOF_BLANK);
                            }
                        }
                    }
                    break;
                case 14:// 对称飞
                    if (game.props["对称飞"] > 0)
                    {
                        game.nr = (byte)(12 - game.row);
                        game.nc = (byte)(12 - game.col);
                        if (game.map[game.layer, game.nr, game.nc] == INDEXOF_BLANK)
                        {
                            game.props["对称飞"]--;
                            UpdateBraverLocation();
                        }
                        if (game.props["对称飞"] == 0)
                            UpdatePictureBox(ind, "空白");
                    }
                    break;
                default: break;
            }
        }
        void GetRound(int r, int c)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pts[i * 3 + j].r = r + i - 1;
                    pts[i * 3 + j].c = c + j - 1;
                }
            }
        }
        short GetAreaRate()
        {
            if (game.layer >= 0 && game.layer < 11)
                return 1;
            else if (game.layer >= 11 && game.layer < 31)
                return 2;
            else if (game.layer >= 31 && game.layer < 41)
                return 4;
            else
                return 5;
        }
        int CalculateCost()
        {
            return 10 * game.ShoppingTime * (game.ShoppingTime - 1) + 20;
        }
        int CalculateDamage()
        {
            int attack = game.braver.attack;
            if ((game.props["十字架"] == -1 && (game.monster.name == "兽人" || game.monster.name == "兽人武士" || game.monster.name == "吸血鬼")) ||
                (game.props["屠龙匕首"] == -1 && game.monster.name == "魔龙"))
                attack += attack;
            if (attack <= game.monster.defense)
                return -1;
            else if (game.braver.defense >= game.monster.attack || game.braver.attack >= game.monster.life + game.monster.defense)
                return 0;
            return game.monster.life / (attack - game.monster.defense) * (game.monster.attack - game.braver.defense);
        }
        bool CalculateMagicDamage()
        {
            int damage = 0;
            bool ret = true;
            if (game.props["神圣盾"] == 0 &&
                game.layer >= 41)
            {
                GetRound(game.nr, game.nc);
                if ((PointDetectEx(pts[1].r, pts[1].c, INDEXOF_KNIGHT_LEADER + 5) &&
                    PointDetectEx(pts[7].r, pts[7].c, INDEXOF_KNIGHT_LEADER + 5)) ||
                    (PointDetectEx(pts[3].r, pts[3].c, INDEXOF_KNIGHT_LEADER + 5) &&
                    PointDetectEx(pts[5].r, pts[5].c, INDEXOF_KNIGHT_LEADER + 5)))
                {
                    damage = game.braver.life / 2;
                }
                else
                {
                    for (int i = 1; i < 8; i += 2)
                    {
                        if (PointDetectEx(pts[i].r, pts[i].c, INDEXOF_KNIGHT_LEADER + 3))
                            damage += 100;
                        else if (PointDetectEx(pts[i].r, pts[i].c, INDEXOF_KNIGHT_LEADER + 4))
                            damage += 200;
                    }
                }
                if (game.braver.life > damage)
                {
                    Tips("受到魔法伤害：" + damage.ToString() + "点生命值。");
                    game.braver.life -= damage;
                    UpdateBraverProperty();
                }
                else
                {
                    ret = false;
                    Tips("生命值过低，无法前往目标区域。");
                }
            }
            return ret;
        }
        void GoUpStair()
        {
            int i = JumpTable[game.layer].上行楼层;
            if (i < MAX_LAYER)
            {
                game.row = JumpTable[game.layer].上行行号;
                game.col = JumpTable[game.layer].上行列号;
                game.layer = (byte)i;
                UpdateFloor();
            }
        }
        void GoDownStair()
        {
            int i = JumpTable[game.layer].下行楼层;
            if (i < MAX_LAYER)
            {
                game.row = JumpTable[game.layer].下行行号;
                game.col = JumpTable[game.layer].下行列号;
                game.layer = (byte)i;
                UpdateFloor();
            }
        }
        void UpdateText(int id, string name)
        {
            Controls[id].Text = name;
        }
        void UpdatePictureBox(int id, string name)
        {
            ((PictureBox)Controls[id]).Image = imgs[name];
        }
        void UpdateMap(int r, int c, string name)
        {
            int idx = r * MAX_COL + c;
            UpdatePictureBox(MAP_OFFSET + idx, name);
        }
        void UpdateMapEx(int l, int r, int c, int ind)
        {
            game.map[l, r, c] = (byte)ind;
            if (game.layer == l)
                UpdateMap(r, c, items[ind]);
        }
        void UpdateFloor()
        {
            if (game.mlayer < game.layer)
                game.mlayer = game.layer;
            DisplayMap(game.layer);
            Controls[3].Text = "第 " + game.layer.ToString() + " 层";
            UpdateMap(game.row, game.col, game.braver.name);
        }
        void UpdateBraverLocation()
        {
            if (game.braver.name != null)
            {
                UpdateMap(game.row, game.col, "空白");
                game.map[game.layer, game.row, game.col] = INDEXOF_BLANK;
                game.map[game.layer, game.nr, game.nc] = INDEXOF_BLANK;
                game.row = game.nr;
                game.col = game.nc;
                UpdateMap(game.row, game.col, game.braver.name);
            }
        }
        void UpdateBraverProperty()
        {
            if (game.braver.name != null)
            {
                Controls[4].Text = game.braver.life.ToString();
                Controls[5].Text = game.braver.attack.ToString();
                Controls[6].Text = game.braver.defense.ToString();
                Controls[7].Text = game.braver.gold.ToString();
            }
        }
        void UpdateMonsterProperty()
        {
            if (game.monster.name != null)
            {
                UpdatePictureBox(MONSTER_ICON_OFFSET, game.monster.name);
                Controls[13].Text = game.monster.name;
                Controls[14].Text = game.monster.life.ToString();
                Controls[15].Text = game.monster.attack.ToString();
                Controls[16].Text = game.monster.defense.ToString();
                Controls[17].Text = game.monster.gold.ToString();
            }
            else
            {
                UpdatePictureBox(MONSTER_ICON_OFFSET, "空白");
                Controls[13].Text = "怪物名字";
                Controls[14].Text = "生命值";
                Controls[15].Text = "攻击力";
                Controls[16].Text = "防御力";
                Controls[17].Text = "金币";
            }
        }
        void UpdateKeysView()
        {
            Controls[10].Text = game.YellowKeyCount.ToString();
            Controls[11].Text = game.BlueKeyCount.ToString();
            Controls[12].Text = game.RedKeyCount.ToString();
        }
        void UpdatePropsView(int ind, string name)
        {
            if (ind == 0)
                UpdatePictureBox(207, name);
            else
                UpdatePictureBox(PICTUREBOX_OFFSET + 3 + ind, name);
        }
        bool GetProp(string name)
        {
            bool ret = false;
            int ind = Array.IndexOf(PropList, name);
            if (ind >= 0)
            {
                if (ind >= 0 && ind < 8)
                    game.props[name] = -1;
                else if (ind >= 8 && ind < 15)
                    game.props[name] = 1;
                else if (ind == 15)
                    game.props[name] = 3;
                UpdatePropsView(ind, name);
                ret = true;
            }
            return ret;
        }
        bool GetEquipment(string name)
        {
            int ind = Array.IndexOf(EquipList, name);
            bool ret = false;
            if (ind >= 0)
            {
                switch (ind)
                {
                    case 0: game.braver.attack += 10; break;
                    case 1: game.braver.attack += 20; break;
                    case 2: game.braver.attack += 40; break;
                    case 3: game.braver.attack += 50; break;
                    case 4: game.braver.attack += 100; break;
                    case 5: game.braver.defense += 10; break;
                    case 6: game.braver.defense += 20; break;
                    case 7: game.braver.defense += 40; break;
                    case 8: game.braver.defense += 50; break;
                    case 9: game.braver.defense += 100; break;
                    default: break;
                }
                if (ind <= 4)
                {
                    UpdatePictureBox(MONSTER_ICON_OFFSET - 5, name);
                    UpdateText(8, name);
                }
                else
                {
                    UpdatePictureBox(MONSTER_ICON_OFFSET - 4, name);
                    UpdateText(9, name);
                }
                UpdateBraverProperty();
                UpdateBraverLocation();

                ret = true;
            }
            return ret;
        }
        bool GetMaterial(string name)
        {
            short rate = GetAreaRate();
            int ind = Array.IndexOf(MaterialList, name);
            bool ret = false;
            if (ind >= 0)
            {
                switch (ind)
                {
                    case 0: game.braver.life += 50 * rate; break;
                    case 1: game.braver.life += 200 * rate; break;
                    case 2: game.braver.attack += rate; break;
                    case 3: game.braver.defense += rate; break;
                    case 4: game.YellowKeyCount++; break;
                    case 5: game.BlueKeyCount++; break;
                    case 6: game.RedKeyCount++; break;
                    default: break;
                }
                if (ind <= 3)
                    UpdateBraverProperty();
                else
                    UpdateKeysView();
                UpdateBraverLocation();
                game.map[game.layer, game.nr, game.nc] = INDEXOF_BLANK;
                ret = true;
            }
            return ret;
        }
        bool GetAction(string name)
        {
            bool ret = true;// 是否需要位置更新
            int ind = Array.IndexOf(ActionList, name);
            if (ind != -1)
            {
                switch (name)
                {
                    case "上行楼梯":
                        GoUpStair();
                        break;
                    case "下行楼梯":
                        GoDownStair();
                        break;
                    case "商店":
                        ind = GetAreaRate();
                        int cost = CalculateCost(), life = 100 * (game.ShoppingTime + 1), attack = ind + ind, defense = attack + attack;
                        shop_dialog.Init(game.braver.gold, cost, life, attack, defense);
                        shop_dialog.ShowDialog();
                        ind = shop_dialog.GetReturn();
                        if (ind != 0)
                        {
                            game.ShoppingTime++;
                            game.braver.gold -= cost;
                            switch (ind)
                            {
                                case 1: game.braver.life += life; break;
                                case 2: game.braver.attack += attack; break;
                                case 3: game.braver.defense += defense; break;
                                default: break;
                            }
                            UpdateBraverProperty();
                        }
                        break;
                    case "黄门":
                        if (game.YellowKeyCount >= 1)
                        {
                            game.YellowKeyCount--;
                            UpdateKeysView();
                            ret = false;
                        }
                        else
                            Tips("你目前没有黄钥匙!");
                        break;
                    case "红门":
                        if (game.RedKeyCount >= 1)
                        {
                            game.RedKeyCount--;
                            UpdateKeysView();
                            ret = false;
                        }
                        else
                            Tips("你目前没有红钥匙!");
                        break;
                    case "蓝门":
                        if (game.BlueKeyCount >= 1)
                        {
                            game.BlueKeyCount--;
                            UpdateKeysView();
                            ret = false;
                        }
                        else
                            Tips("你目前没有蓝钥匙!");
                        break;
                    case "智者":
                        switch (game.layer)
                        {
                            case 2: L2_R4_C11(); break;
                            case 3: L3_R4_C11(); break;
                            default:
                                GeneralTalkDialog(default_strs);
                                UpdateMapEx(game.layer, game.nr, game.nc, INDEXOF_BLANK);
                                break;
                        }
                        break;
                    case "商人":
                        switch (game.layer)
                        {
                            case 2: L2_R7_C11(); break;
                            case 6: L6_R4_C8(); break;
                            case 7: L7_R1_C6(); break;
                            case 12:
                                if (game.col < 6)
                                    L12_R1_C1();
                                else
                                    L12_R1_C11();
                                break;
                            case 15: L15_R11_C11(); break;
                            case 28: L28_R4_C8(); break;
                            case 38: L38_R2_C5(); break;
                            case 39: L39_R2_C9(); break;
                            case 45: L45_R3_C9(); break;
                            case 47: L47_R2_C5(); break;
                            default: break;
                        }
                        break;
                    case "小偷":
                        switch (game.layer)
                        {
                            case 2: L2_R7_C3(); break;
                            case 15: L15_R1_C9(); break;
                            case 29: L29_R2_C6(); break;
                            case 35: L35_R9_C5(); break;
                            default: break;
                        }
                        break;
                    case "公主":
                        L26_R6_C6();
                        break;
                    default: break;
                }
            }
            else
                ret = false;
            return ret;
        }
        bool Battle(string name)
        {
            bool ret = false;
            int ind = Array.IndexOf(MonsterList, tmp), damage;
            if (ind != -1)
            {
                game.monster = entities[ind];
                damage = CalculateDamage();

                if (damage == -1)
                {
                    ret = true;
                    Tips("攻击力不够，无法攻击！");
                }
                else if (game.braver.life > damage)
                {
                    game.braver.life -= damage;
                    if (game.props["幸运金币"] == -1)
                        game.braver.gold += 2 * game.monster.gold;
                    else
                        game.braver.gold += game.monster.gold;
                    UpdateMonsterProperty();
                    UpdateBraverProperty();
                    game.monster.name = null;
                    UpdateBraverLocation();
                }
                else
                {
                    ret = true;
                    Tips("血量不足，无法攻击");
                }
            }
            else
                UpdateMonsterProperty();
            return ret;
        }
        void BraverMove(DIRECTION direction)
        {
            // 每次移动都刷新提示栏和怪物信息区
            Tips("");
            UpdateMonsterProperty();
            // 勇者方向和位置更新
            if (game.dir != direction)
            {
                game.dir = direction;
                switch (game.dir)
                {
                    case DIRECTION.LEFT: game.braver.name = "勇者2"; break;
                    case DIRECTION.RIGHT: game.braver.name = "勇者3"; break;
                    case DIRECTION.UP: game.braver.name = "勇者4"; break;
                    case DIRECTION.DOWN: game.braver.name = "勇者1"; break;
                    default: break;
                }
            }
            game.nr = game.row;
            game.nc = game.col;
            switch (game.dir)
            {
                case DIRECTION.LEFT: game.nc--; break;
                case DIRECTION.RIGHT: game.nc++; break;
                case DIRECTION.UP: game.nr--; break;
                case DIRECTION.DOWN: game.nr++; break;
                default: break;
            }
            // 获取目标位置方块，如果为不可达方块，直接退出
            tmp = items[game.map[game.layer, game.nr, game.nc]];
            int ind = Array.IndexOf(UnreachableList, tmp);
            if (ind == -1)
            {
                if (!CalculateMagicDamage())
                    return;
                GetProp(tmp);
                if (GetEquipment(tmp) || GetMaterial(tmp) || Battle(tmp) || GetAction(tmp))
                    return;
                UpdateBraverLocation();
                EventDetect();
            }
            else
                UpdateMap(game.row, game.col, game.braver.name);
        }
    }
}
