using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace KirikiriTalk.Tool
{
    public class IntPass : UnityEvent<int> { }

    public class GuassRandom
    {
        static float[,] weightRangePair;//weight,number
        static int weightMax = 1000;
        static int range = 1000;
        static float totalWeight;

        static GuassRandom()
        {
            weightRangePair = new float[range, 2];
            totalWeight = 0;

            for (int i = 0; i < range; i++)
            {
                weightRangePair[i, 0] = Mathf.Sin((float)i / range * Mathf.PI) * weightMax;
                //Debug.Log("Mathf.Sin((float)i / range * Mathf.PI)  " + Mathf.Sin((float)i / range * Mathf.PI));
                totalWeight += weightRangePair[i, 0];
                weightRangePair[i, 1] = i;
            }
        }

        /// <summary>
        /// random in 0~999
        /// </summary>
        /// <returns>0~999</returns>
        public static int GetNextGuass()
        {
            float totalWeight = 0;
            int random = UnityEngine.Random.Range(0, Mathf.CeilToInt(GuassRandom.totalWeight));
            //Debug.Log("random       " + random);
            //Debug.Log("totalWeight  " + GuassRandom.totalWeight);
            for (int i = 0; i < range; i++)
            {
                totalWeight += weightRangePair[i, 0];
                if (Mathf.CeilToInt(totalWeight) >= random)
                {
                    return Mathf.CeilToInt(weightRangePair[i, 1]);
                }
            }
            return -1;
        }

        public static float GetNextGuassFloat()
        {
            return (float)GetNextGuass() / range;
        }

        public static float GetGuassRange(float min, float max)
        {
            return min + GetNextGuassFloat() * (max - min);
        }
    }

    public class Tools
    {
        [Obsolete]
        public static Vector3 MoveLerpVector2(Vector3 current, Vector3 target, float lerp)
        {
            return new Vector3(Mathf.Lerp(current.x, target.x, lerp), Mathf.Lerp(current.y, target.y, lerp), current.z);
        }

        [Obsolete]
        public static Vector3 MoveTowardsVector2(Vector3 current, Vector3 target, float speed)
        {
            return new Vector3(Mathf.MoveTowards(current.x, target.x, speed), Mathf.MoveTowards(current.y, target.y, speed), current.z);
        }

        [Obsolete]
        public static Vector3 MoveTowardsVector3(Vector3 current, Vector3 target, float speed)
        {
            return new Vector3(Mathf.MoveTowards(current.x, target.x, speed), Mathf.MoveTowards(current.y, target.y, speed), Mathf.MoveTowards(current.z, target.z, speed));
        }

        /// <summary>
        /// 經過deltaTime後固定回傳True
        /// </summary>
        /// <param name="time">開始計時的時間。給0則第一次計時並回傳false</param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public static bool CheckDeltaTime(ref float time, float deltaTime)
        {
            if (time == 0)
            {
                time = Time.time;
                return false;
            }
            else if ((time + deltaTime) < Time.time)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 每次間隔回傳一次True
        /// </summary>
        /// <param name="time">開始計時的時間。給0則第一次計時並回傳false</param>
        /// <param name="deltaTime">時間間隔</param>
        /// <returns></returns>
        public static bool CheckPerDeltaTime(ref float time, float deltaTime)
        {
            if (time == 0)
            {
                time = Time.time;
                return false;
            }
            if (Time.time >= time + deltaTime)
            {
                time = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T[] GetAllChild<T>(Transform parent)
        {
            List<T> list = new List<T>();
            T comp;
            comp = parent.GetComponent<T>();
            if (comp != null&&!comp.Equals(default(T)))
            {
                list.Add(comp);
            }

            for (int i = 0; i < parent.childCount; i++)
            {
                list.AddRange(GetAllChild<T>(parent.GetChild(i)));
            }

            return list.ToArray();
        }

        public static Vector2 GetScreenSizeInWoldSize()
        {
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            return new Vector2(width, height);
        }

        public static string GetHierarchyPath(Transform target)
        {
            if (target.parent == null)
            {
                return target.name;
            }else
            {
                return GetHierarchyPath(target.parent) +"/"+target.name;
            }
        }
    }

    public class SuperQueue<T> : Queue<T>
    {
        public void RemoveParamet(T target)
        {
            Queue<T> temp = new Queue<T>();
            while (this.Count != 0)
            {
                if (!this.Peek().Equals(target))
                {
                    temp.Enqueue(this.Dequeue());
                }
                else
                {
                    this.Dequeue();
                }
            }
            while (temp.Count != 0)
            {
                this.Enqueue(temp.Dequeue());
            }
        }
    }

    public class MultiValueDictionary<Key, Value> : Dictionary<Key, List<Value>>
    {
        public void Add(Key k, Value v)
        {
            if (base.ContainsKey(k))
            {
                base[k].Add(v);
            }
            else
            {
                List<Value> t = new List<Value>();
                t.Add(v);
                base.Add(k, t);
            }
        }
        public bool ContainsValue(Value v)
        {
            Dictionary<Key, List<Value>> k = base.MemberwiseClone() as Dictionary<Key, List<Value>>;

            foreach (KeyValuePair<Key, List<Value>> kp in k)
            {
                foreach (Value val in kp.Value)
                {
                    if (val.Equals(v))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int AllValueCount
        {
            get
            {
                int n = 0;
                Dictionary<Key, List<Value>> k = base.MemberwiseClone() as Dictionary<Key, List<Value>>;
                foreach (KeyValuePair<Key, List<Value>> kp in k)
                {
                    n += kp.Value.Count;
                }
                return n;
            }
        }
        public int ValueCount(Key k)
        {
            if (base.ContainsKey(k))
            {
                return base[k].Count;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更改指定Value的Key值
        /// </summary>
        /// <param name="value">欲更改之Value</param>
        /// <param name="key">新的Key </param>
        /// <returns></returns>
        public bool ChangeKey(Value value, Key key)
        {
            if (this.ContainsValue(value))
            {
                Dictionary<Key, List<Value>> b = base.MemberwiseClone() as Dictionary<Key, List<Value>>;
                //移除舊資料
                this.RemoveValue(value);
                //新增新資料
                this.Add(key, value);

                return true;
            }
            else
            {
                return false;
            }
        }
        public Value[] AllValue(Key k)
        {
            return base[k].ToArray();
        }
        public new Value this[Key k]
        {
            get
            {
                return base[k][0];
            }
        }
        public void RemoveValue(Value value)
        {

            Dictionary<Key, List<Value>> b = base.MemberwiseClone() as Dictionary<Key, List<Value>>;
            foreach (KeyValuePair<Key, List<Value>> kp in b)
            {
                if (kp.Value.Exists((x) => value.Equals(x)))
                {
                    kp.Value.Remove(value);
                    if (kp.Value.Count == 0)
                    {
                        base.Remove(kp.Key);
                    }
                }
            }
        }
        public new IEnumerator GetEnumerator()
        {
            Dictionary<Key, List<Value>> baseDictionary = base.MemberwiseClone() as Dictionary<Key, List<Value>>;
            foreach (KeyValuePair<Key, List<Value>> BasKP in baseDictionary)
            {
                foreach (Value v in BasKP.Value)
                {
                    KeyValuePair<Key, Value> kp = new KeyValuePair<Key, Value>(BasKP.Key, v);
                    yield return kp;
                }
            }
        }

        public bool TryFindKey(Predicate<Value> match, out Key k)
        {
            Dictionary<Key, List<Value>> b = base.MemberwiseClone() as Dictionary<Key, List<Value>>;
            //移除舊資料
            foreach (KeyValuePair<Key, List<Value>> kp in b)
            {
                foreach (Value v in kp.Value)
                {
                    if (match(v))
                    {
                        k = kp.Key;
                        return true;
                    }
                }
            }
            k = default(Key);
            return false;
        }



    }
    
}

