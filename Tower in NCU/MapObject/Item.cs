﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Applet;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Image;
using Tower_in_NCU.Audio;

namespace Tower_in_NCU.MapObject
{
    class Item : MapObject
    {
        public Item(ImageUnit img, MapObjectType type) : base(img, type) { }

        public Item(List<ImageUnit> frames, MapObjectType type) : base(frames, type) { }
        
        public override bool Event(Player player, Floor floor)
        {
            bool access = true;
            switch (_type)
            {
                case MapObjectType.Floor1:
                    break;
                case MapObjectType.Block1:
                    access = false;
                    break;
                case MapObjectType.DownStair1:
                case MapObjectType.DownStair2:
                    player.CurrentFloor--;
                    break;
                case MapObjectType.UpStair1:
                case MapObjectType.UpStair2:
                    player.CurrentFloor++;
                    break;
                case MapObjectType.YellowKey:
                    player.YellowKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "獲得一把黃鑰匙", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.BlueKey:
                    player.BlueKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "獲得一把藍鑰匙", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.RedKey:
                    player.RedKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "獲得一把紅鑰匙", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.MonsterBook:
                    return true;
                case MapObjectType.RedPotion:
                    player.Hp += 150;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "生命增加 150 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.BluePotion:
                    player.Hp += 500;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "生命增加 500 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.RedCrystal:
                    player.Atk += 2;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "攻擊增加 2 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.BlueCrystal:
                    player.Def += 2;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "防禦增加 2 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.YellowDoor:
                    if(player.YellowKey > 0)
                    {
                        player.YellowKey--;
                        floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                        _audioPlayer.Play(AudioPlayer.SoundEffect.OpenDoor);
                        break;
                    }
                    else
                    {
                        access = false;
                        break;
                    }
                case MapObjectType.BlueDoor:
                    if (player.BlueKey > 0)
                    {
                        player.BlueKey--;
                        floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                        _audioPlayer.Play(AudioPlayer.SoundEffect.OpenDoor);
                        break;
                    }
                    else
                    {
                        access = false;
                        break;
                    }
                case MapObjectType.RedDoor:
                    if (player.RedKey > 0)
                    {
                        player.RedKey--;
                        floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                        _audioPlayer.Play(AudioPlayer.SoundEffect.OpenDoor);
                        break;
                    }
                    else
                    {
                        access = false;
                        break;
                    }
                case MapObjectType.TeleportStaf:
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.Sword1:
                    player.Atk += 10;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "取得鐵劍，攻擊增加 10 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.Sword2:
                case MapObjectType.Sword3:
                case MapObjectType.Sword4:
                case MapObjectType.Sword5:
                case MapObjectType.Shield1:
                    player.Def += 8;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "系統資訊", "取得鐵盾，防禦增加 8 點", Dialogue.FaceLoaction.None, null);
                    _audioPlayer.Play(AudioPlayer.SoundEffect.GetItem);
                    break;
                case MapObjectType.Shield2:
                case MapObjectType.Shield3:
                case MapObjectType.Shield4:
                case MapObjectType.Shield5:
                    break;
            }
            return access;
        }
    }
}