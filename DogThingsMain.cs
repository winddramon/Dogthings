using BepInEx;
using HarmonyLib;
using RshLib;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

namespace DogThings
{
    [BepInPlugin("Yoshiko_G.DogThings", "DogThings", "1.2.2")]
    [BepInDependency("com.rushellxyz.rshlib", BepInDependency.DependencyFlags.HardDependency)]

    public class DogThingsMain : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony harmony = new Harmony("Yoshiko_G.DogThings");
            harmony.PatchAll();

            //dog collar
            ItemInfo iteminfo = new ItemInfo
            {
                category = "utility",
                slotRotation = 0f,
                usable = false,
                usableOnLimb = false,
                decayMinutes = 360f,
                decayInfo = 6,
                destroyAtZeroCondition = true,
                wearable = true,
                wearableCanBeHeld = true,
                combineable = false,
                desiredWearLimb = "UpTorso",
                wearSlotId = "neck",
                weight = 0.2f,
                wearableIsolation = 0.03f,
                wearableVisualOffset = 8,
                value = 20,
                fullName = GetText("dogthings.dogcollar", "Dog Collar", "狗项圈"),
                description = GetText("dogthings.dogcollar.dsc", "This thing around your neck makes you feel like a good boy.", "戴在脖子上就能让你感觉自己是个乖宝宝。"),
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.dogcollar", new RshLib.RshItem
            {
                sprite = LoadSprite("dogcollar"),
                info = iteminfo,
            });

            //dog bowl
            iteminfo = new LiquidItemInfo
            {
                category = "custom",
                slotRotation = -45f,
                usable = true,
                usableOnLimb = false,
                weight = 0.5f,
                value = 5,
                capacity = 200f,
                defaultContents = new List<LiquidStack>(),
                useAction = delegate (Body body, Item item)
                {
                    var wc = item.GetComponent<WaterContainerItem>();
                    var ttlBefore = 0f;
                    var ttlAfter = 0f;
                    foreach (var lq in wc.stack) ttlBefore += lq.amount;
                    wc.Drink(body, 100f, "drink");
                    foreach (var lq in wc.stack) ttlAfter += lq.amount;
                    if(ttlAfter < ttlBefore) body.happiness += 3f;
                },
                fullName = GetText("dogthings.dogbowl", "Dog bowl", "狗食盆"),
                description = GetText("dogthings.dogbowl.dsc", "If you get something from this bowl, you get a little bonus happy.", "从里面喝东西会让你感觉有点小开心。"),
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.dogbowl", new RshLib.RshItem
            {
                sprite = LoadSprite("dogbowl"),
                info = iteminfo,
                onSpawn = (gObject, args) =>
                {
                    gObject.AddComponent<WaterContainerItem>();
                }
            });

            //pet ball
            iteminfo = new ItemInfo
            {
                category = "utility",
                slotRotation = -45f,
                usable = false,
                usableOnLimb = false,
                decayMinutes = 360f,
                //decayInfo = 6,
                destroyAtZeroCondition = true,
                combineable = false,
                weight = 0.75f,
                value = 12,
                fullName = GetText("dogthings.petball", "Pet ball", "宠物球"),
                description = GetText("dogthings.petball.dsc", "Bring it back like a champion.", "把它捡回来似乎是一件非常值得骄傲的事情。"),
            };
            iteminfo.SetTags();
            RshLib.Plugin.RegisterItem("dogthings.petball", new RshLib.RshItem
            {
                sprite = LoadSprite("petball"),
                info = iteminfo,
            });

            //Elizabethan collar
            iteminfo = new ItemInfo
            {
                category = "utility",
                slotRotation = 0f,
                usable = false,
                usableOnLimb = false,
                decayMinutes = 60f,
                decayInfo = 6,
                destroyAtZeroCondition = true,
                wearable = true,
                wearableCanBeHeld = true,
                combineable = false,
                desiredWearLimb = "UpTorso",
                wearSlotId = "neck",
                weight = 1.5f,
                wearableIsolation = 0.05f,
                wearableVisualOffset = 100,
                value = 5,
                fullName = GetText("dogthings.e-collar", "Elizabethan collar", "伊丽莎白圈"),
                description = GetText("dogthings.e-collar.dsc", "Stops you from doing something very stupid. Once.", "阻止你做傻事……仅限一次。"),
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.e-collar", new RshLib.RshItem
            {
                sprite = LoadSprite("e-collar"),
                info = iteminfo,
            });

            //dog chew
            iteminfo = new ItemInfo
            {
                category = "food",
                slotRotation = 0f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 360f,
                //decayInfo = 6,
                destroyAtZeroCondition = true,
                combineable = false,
                weight = 0.6f,
                scaleWeightWithCondition = true,
                value = 15,
                fullName = GetText("dogthings.dogchew", "Dog chew", "狗咬胶"),
                description = GetText("dogthings.dogchew.dsc", "Chew a little. Feel a little better. Again. And again.", "可以啃食很多下。好吃！"),
                useAction = delegate (Body body, Item item)
                {
                    //body.Drink(2f);
                    body.Eat(0.5f, 0.01f);
                    body.happiness += 0.5f;
                    item.condition -= 0.05f;
                    Sound.Play("eatCrunch", body.transform.position, false, true, null, 1f, 1f, false, false);
                },
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.dogchew", new RshLib.RshItem
            {
                sprite = LoadSprite("dogchew"),
                info = iteminfo,
            });

            //Dog carrier
            iteminfo = new ItemInfo
            {
                category = "container",
                slotRotation = 0f,
                usable = false,
                usableOnLimb = false,
                decayMinutes = 300f,
                decayInfo = 6,
                destroyAtZeroCondition = true,
                wearable = true,
                wearableCanBeHeld = false,
                combineable = false,
                desiredWearLimb = "UpTorso",
                wearSlotId = "back",
                weight = 2.5f,
                wearableIsolation = 0.1f,
                wearableVisualOffset = 5,
                value = 80,
                fullName = GetText("dogthings.dogcarrier", "Dog carrier", "狗狗背包"),
                description = GetText("dogthings.dogcarrier.dsc", "It's big enough to carry you.", "巨大的背包，大到能把你都装进去。"),
                rec = new Recognition(4),
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.dogcarrier", new RshLib.RshItem
            {
                sprite = LoadSprite("dogcarrier"),
                info = iteminfo,
                onSpawn = (gObject, args) =>
                {
                    var c = gObject.AddComponent<Container>();
                    c.maxWeight = 50f;
                    c.maxWeightPerItem = 50f;
                    c.encumberanceMult = 0.25f;
                }
            });

            //Poop bag
            iteminfo = new ItemInfo
            {
                category = "container",
                slotRotation = 0f,
                usable = false,
                usableOnLimb = false,
                decayMinutes = 60f,
                decayInfo = 6,
                destroyAtZeroCondition = true,
                combineable = false,
                weight = 0.2f,
                value = 10,
                fullName = GetText("dogthings.poopbag", "Poop bag", "捡屎袋"),
                description = GetText("dogthings.poopbag.dsc", "One bag. Many messes.", "一个袋子可以装下许多麻烦。"),
                rec = new Recognition(3),
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.RegisterItem("dogthings.poopbag", new RshLib.RshItem
            {
                sprite = LoadSprite("poopbag"),
                info = iteminfo,
                onSpawn = (gObject, args) =>
                {
                    var c = gObject.AddComponent<Container>();
                    c.maxWeight = 8f;
                    c.maxWeightPerItem = 8f;
                    c.encumberanceMult = 0.10f;
                    c.tagRestriction = new[] { "droppings" };
                }
            });
        }

        private static Sprite LoadSprite(string id, float ppu = 8.0f)
        {
            byte[] fileData = File.ReadAllBytes($"BepInEx/plugins/dogthings/img/{id}.png");
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(fileData);
            texture.filterMode = FilterMode.Point;

            // 默认 pivot 为中心
            Vector2 pivot = new Vector2(0.5f, 0.5f);

            // 特殊道具单独设置
            if ("e-collar" == id)
                pivot = new Vector2(0.5f, 0f);  // 伊丽莎白圈：底部正中
            else if ("dogcarrier" == id)
                pivot = new Vector2(0.8f, 0.5f);  // 狗狗背包
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, ppu);
        }

        private static string GetText(string key, string en, string zhCN)
        {
            string currentLang = PlayerPrefs.GetString("locale", "EN");
            //Debug.Log("当前语言：" + currentLang);
            return currentLang switch
            {
                "zh_CN" => zhCN,
                "zh-CN" => zhCN,
                _ => en
            };
        }
    }

    [HarmonyPatch(typeof(CustomItemBehaviour), "Update")]
    class CustomItemBehaviour_Update_Patch
    {
        private static void Postfix(CustomItemBehaviour __instance)
        {
            if (__instance != null)
            {
                Item item = __instance.GetComponent<Item>();

                if ("dogthings.dogcollar" == item.id)
                {
                    if (item.transform.parent && item.transform.parent.name == "UpTorso")
                    {
                        Limb limb = null;
                        if (item.transform.parent.TryGetComponent<Limb>(out limb))
                        {
                            if(limb.body.happiness < 30) limb.body.happiness += Time.deltaTime * 0.08f;
                            //Debug.Log("已成功执行3.");
                        }
                    }
                    return;
                }
                else if("dogthings.petball" == item.id)
                {
                    //Debug.Log("道具state数值为：" + __instance.state);
                    //0为初始和无主状态，2为捡起状态（装进袋子不行）。其他状态值也归为无主状态。

                    if (item.transform.parent)//捡起状态
                    {
                        InventorySlot inv = null;
                        if (__instance.state == 0 || __instance.state != 2)
                        {
                            if(__instance.state != 0) Debug.Log("pet ball state异常。数值为：" + __instance.state);

                            if (item.transform.parent.TryGetComponent<InventorySlot>(out inv))//捡起状态
                            {
                                //如果原本无主，增加心情值
                                if (inv.body.happiness < 50) inv.body.happiness += 0.5f;
                                //Debug.Log("增加心情值");
                            }
                        }
                        if (__instance.state != 2)
                        {
                            __instance.state = 2;
                            //Debug.Log("将道具state数值改为：" + __instance.state);
                        }
                    }
                    else //无主状态
                    {
                        if (__instance.state != 0)
                        {
                            __instance.state = 0;
                            //Debug.Log("将道具state数值改为：" + __instance.state);
                        }
                    }
                    return;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Item), "Start")]
    class Item_Start_Patch
    {
        private static HashSet<string> NeedsCustomItemBehaviour = new HashSet<string>
        {
            "dogthings.dogcollar", "dogthings.petball",
        };

        static void Postfix(Item __instance)
        {
            if (__instance != null)
            {
                if (NeedsCustomItemBehaviour.Contains(__instance.id) && __instance.GetComponent<CustomItemBehaviour>() == null)
                {
                    __instance.gameObject.AddComponent<CustomItemBehaviour>();
                }

                //以下是某些需要单独修改资源层的特殊道具

                //edit pet ball's bounciness
                if ("dogthings.petball" == __instance.id)
                {
                    var rb = __instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // 复制或创建材质
                        var material = rb.sharedMaterial != null
                            ? Object.Instantiate(rb.sharedMaterial)
                            : new PhysicsMaterial2D();

                        material.bounciness = 0.85f;
                        rb.drag = 0.6f;
                        rb.angularDrag = 1.0f;
                        //rb.sleepMode = RigidbodySleepMode2D.StartAsleep;

                        material.name = "PetBall_BouncyMat";

                        rb.sharedMaterial = material;
                        //Debug.Log($"pet ball's bounciness set to {material.bounciness}");
                    }
                }

                //一些调试用
                //if("dogthings.poopbag" == __instance.id)
                //{
                //    var c = __instance.GetComponent<Container>();
                //    Debug.Log("屎袋信息：" + string.Join(", ", c.tagRestriction));
                //}
                //Debug.Log(__instance.id + " 道具标签:" + string.Join(", ", __instance.Stats.GetTags()));
                //if ("droppings" == __instance.id)
                //{
                //    Debug.Log(Item.GlobalItems["droppings"].tags);
                //}
            }
        }
    }

    [HarmonyPatch(typeof(Item), "SetupItems")]
    class Item_SetupItems_Patch
    {
        static void Postfix()
        {
            if (Item.GlobalItems.ContainsKey("droppings"))
            {
                Item.GlobalItems["droppings"].tags = "droppings";
                Item.GlobalItems["droppings"].SetTags();
                //Debug.Log("已为屎成功添加标签。");
            }
        }
    }

    [HarmonyPatch(typeof(SelfHarmer), "AttemptHarm")]
    [HarmonyPriority(Priority.High)]
    class SelfHarmer_AttemptHarm_Patch()
    {
        static bool Prefix(SelfHarmer __instance)
        {
            bool res = true;
            var fieldInfo = AccessTools.Field(typeof(SelfHarmer), "body");
            Body body = (Body)fieldInfo.GetValue(__instance);
            Item item;
            if (body.FindByIdThorough("dogthings.e-collar", out item))
            {
                if(item.transform.parent && item.transform.parent.name == "UpTorso")
                {
                    __instance.cooldownTime = 180f;
                    item.condition = 0f;
                    var methodInfo = AccessTools.Method(typeof(SelfHarmer), "ClawAnim");
                    methodInfo.Invoke(__instance, null);
                    body.happiness += 3f;
                    body.talker.Talk(Locale.GetCharacter("angeronitem"), null, true, true);
                    res = false;
                }
                
            }
            return res;
        }
    }



}