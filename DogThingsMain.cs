using BepInEx;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DogThings
{
    [BepInPlugin("Yoshiko_G.DogThings", "DogThings", "1.0.0")]
    [BepInDependency("com.rushellxyz.rshlib", BepInDependency.DependencyFlags.HardDependency)]

    public class DogThingsMain : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony harmony = new Harmony("Yoshiko_G.DogThings");
            harmony.PatchAll();
            
            //dog collar
            RshLib.Plugin.customItems.Add("dogthings.dogcollar", LoadSprtie("dogcollar"));
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
                fullName = "Dog collar",
                description = "This thing around your neck makes you feel like a good boy.",
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.customInfos.Add("dogthings.dogcollar", iteminfo);

            //dog bowl
            RshLib.Plugin.customItems.Add("dogthings.dogbowl", LoadSprtie("dogbowl"));
            iteminfo = new LiquidItemInfo
            {
                category = "custom",
                slotRotation = -45f,
                usable = true,
                usableOnLimb = false,
                //decayMinutes = 360f,
                //decayInfo = 6,
                //destroyAtZeroCondition = false,
                //combineable = true,
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
                fullName = "Dog bowl",
                description = "If you get something from this bowl, you get a little bonus happy.",
            };
            iteminfo.SetTags();
            RshLib.Plugin.customInfos.Add("dogthings.dogbowl", iteminfo);

            //pet ball
            RshLib.Plugin.customItems.Add("dogthings.petball", LoadSprtie("petball"));
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
                fullName = "Pet ball",
                description = "Bring it back like a champion.",
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.customInfos.Add("dogthings.petball", iteminfo);

            //Elizabethan collar
            RshLib.Plugin.customItems.Add("dogthings.e-collar", LoadSprtie("e-collar"));
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
                fullName = "Elizabethan collar",
                description = "Stops you from doing something very stupid. Once.",
            };
            iteminfo.SetTags();
            if (iteminfo.decayMinutes > 0f)
            {
                iteminfo.rotSpeed = 1.666f / iteminfo.decayMinutes;
            }
            RshLib.Plugin.customInfos.Add("dogthings.e-collar", iteminfo);

            //dog chew
            RshLib.Plugin.customItems.Add("dogthings.dogchew", LoadSprtie("dogchew"));
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
                fullName = "Dog chew",
                description = "Chew a little. Feel a little better. Again. And again.",
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
            RshLib.Plugin.customInfos.Add("dogthings.dogchew", iteminfo);
        }

        static Sprite LoadSprtie(string id, float ppu = 8.0f)
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

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, ppu);
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
    [HarmonyPriority(Priority.High)]
    class Item_Start_Patch
    {
        private static HashSet<string> NeedsCustomItemBehaviour = new HashSet<string>
        {
            "dogthings.dogcollar", "dogthings.petball",
        };

        private static HashSet<string> NeedsWaterContainerItem = new HashSet<string>
        {
            "dogthings.dogbowl",
        };
        static void Postfix(Item __instance)
        {
            if (__instance != null)
            {
                if (NeedsCustomItemBehaviour.Contains(__instance.id) && __instance.GetComponent<CustomItemBehaviour>() == null)
                {
                    __instance.gameObject.AddComponent<CustomItemBehaviour>();
                    //edit pet ball's bounciness
                    if ("dogthings.petball" == __instance.id)
                    {
                        var rb = __instance.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            //Debug.Log($"=== Pet Ball Rigidbody2D 信息 ===");
                            //Debug.Log($"mass (质量): {rb.mass}");
                            //Debug.Log($"drag (线性阻尼): {rb.drag}");
                            //Debug.Log($"angularDrag (角阻尼): {rb.angularDrag}");
                            //Debug.Log($"gravityScale (重力缩放): {rb.gravityScale}");
                            //Debug.Log($"bounciness (弹性): {rb.sharedMaterial?.bounciness ?? 0}");  // 需要获取材质
                            //Debug.Log($"friction (摩擦力): {rb.sharedMaterial?.friction ?? 0}");
                            //Debug.Log($"material 名称: {rb.sharedMaterial?.name ?? "null"}");
                            //Debug.Log($"bodyType (刚体类型): {rb.bodyType}");
                            //Debug.Log($"constraints (约束): {rb.constraints}");
                            //Debug.Log($"collisionDetectionMode (碰撞检测): {rb.collisionDetectionMode}");
                            //Debug.Log($"interpolation (插值): {rb.interpolation}");
                            //Debug.Log($"sleepMode (休眠模式): {rb.sleepMode}");

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

                            Debug.Log($"pet ball's bounciness set to {material.bounciness}");
                        }
                    }
                }

                if (NeedsWaterContainerItem.Contains(__instance.id) && __instance.GetComponent<WaterContainerItem>() == null)
                {
                    __instance.gameObject.AddComponent<WaterContainerItem>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(SelfHarmer), "AttemptHarm")]
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