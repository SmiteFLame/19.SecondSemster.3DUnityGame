using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Threading;

public static class DataSerializer
{
    public static void SaveToFile(object Target, string Filename, string SubDirectory = null)
    {
        string RealPath = Application.persistentDataPath;
        if (SubDirectory != null)
        {
            RealPath = Path.Combine(Application.persistentDataPath, SubDirectory);
            if (!Directory.Exists(RealPath))
                Directory.CreateDirectory(RealPath);
        }
        RealPath = Path.Combine(RealPath, Filename + ".dat");
        using (FileStream Fs = new FileStream(RealPath, FileMode.CreateNew))
        {
            BinaryFormatter Bformatter =
               new BinaryFormatter();
            try
            {
                Bformatter.Serialize(Fs, Target);
            }
            catch (SerializationException E)
            {
                Debug.LogError(E.Message);
            }
            finally
            {
                Fs.Close();
            }
        }

    }
    public static T ReadFromFile<T>(string Filename, string SubDirectory = null)
    {
        string RealPath = Application.persistentDataPath;
        if (SubDirectory != null)
        {
            RealPath = Path.Combine(Application.persistentDataPath, SubDirectory);
            if (!Directory.Exists(RealPath))
                Directory.CreateDirectory(RealPath);
        }
        RealPath = Path.Combine(RealPath, Filename + ".dat");
        T Output = default(T);
        try
        {
            using (FileStream Fs = new FileStream(RealPath, FileMode.Open))
            {
                BinaryFormatter Bformatter =
                   new BinaryFormatter();
                try
                {
                    Output = (T)Bformatter.Deserialize(Fs);
                }
                catch (SerializationException E)
                {
                    Debug.LogError(E.Message);
                    throw new System.Exception("파일이 손상되었거나 접근할 수 없습니다.");
                }
                finally
                {
                    Fs.Close();
                }
            }
            return Output;
        }
        catch (FileNotFoundException)
        {
            Debug.LogError("File Not Found @ Path : " + RealPath);
            throw new System.Exception("파일을 찾을 수 없습니다. 접근경로 : " + RealPath);
        }
    }
    public static T ReadFromResource<T>(string Filename)
    {
        T Obj = default(T);
        try
        {
            TextAsset LoadedText = Resources.Load(Filename) as TextAsset;
            Debug.Log(LoadedText.bytes.Length);
            using (MemoryStream ms = new MemoryStream(LoadedText.bytes))
            {
                BinaryFormatter Bformatter = new BinaryFormatter();
                Obj = (T)Bformatter.Deserialize(ms);
            }
            return Obj;
        }
        catch (System.Exception E)
        {
            Debug.LogError("Error : " + E.Message);
            throw;
        }
    }

}

[System.Serializable]
public class UsrProfile
{
    public string Name;
    public char Gender;
    public float Height;

    private static UsrProfile _Instance;
    public static UsrProfile Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new UsrProfile();
            return _Instance;
        }
        set
        {
            _Instance = value;
        }
    }
    public static UsrProfile GetInstance(string name = "User")
    {
        if (_Instance == null)
            _Instance = new UsrProfile(name);
        else
            _Instance.Name = name;
        return _Instance;
    }

    public UsrProfile(string UserName = "User")
    {
        Name = UserName;
        Gender = 'M';
        Height = 1.7f;
    }
    public void Save()
    {
        DataSerializer.SaveToFile(this, "User");
    }
    public void Load(string Filename)
    {
        UsrProfile Loaded = DataSerializer.ReadFromFile<UsrProfile>(Filename);
        Name = Loaded.Name;
        Gender = Loaded.Gender;
        Height = Loaded.Height;
    }
}
[System.Serializable]
public class SensorPoint
{
    public float x;
    public float y;
    public float z;
    public SensorPoint()
    {
        x = 0;
        y = 0;
        z = 0;
    }
    public SensorPoint(float nx, float ny, float nz)
    {
        x = nx;
        y = ny;
        z = nz;
    }
    public SensorPoint(float nx, float ny)
    {
        x = nx;
        y = ny;
        z = 0;
    }
    public SensorPoint(SensorPoint Other)
    {
        x = Other.x;
        y = Other.y;
        z = Other.z;
    }
    public float GetPow()
    {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));
    }
    public static SensorPoint operator +(SensorPoint A, SensorPoint B)
    {
        return new SensorPoint(A.x + B.x, A.y + B.y, A.z + B.z);
    }
    public static SensorPoint operator -(SensorPoint A, SensorPoint B)
    {
        return new SensorPoint(A.x - B.x, A.y - B.y, A.z - B.z);
    }
}
[System.Serializable]
public class SensorValue
{
    public enum SV_Status
    {
        // 지정되지 않음
        NA,
        // 공을 치기 전
        PreSwing,
        // 공을 치는 중 / 친 직후
        SwingHit,
        // 공을 치고서 시간이 지남
        AfterSwing,
    }
    /// <summary>
    /// 이 센서값의 상태를 나타냄
    /// </summary>
    public SV_Status Status;
    /// <summary>
    /// 가속도 (x,y,z)
    /// </summary>
    public SensorPoint Acceleration;
    /// <summary>
    /// 각속도 (x,y,z)
    /// </summary>
    public SensorPoint AngularVelocity;
    /// <summary>
    /// 좌표 (x,y,z)
    /// </summary>
    public SensorPoint Position;
    /// <summary>
    /// 센서의 값 (float 리스트) 를 자동으로 3개의 벡터로 변환
    /// </summary>
    /// <param name="Datas">입력할 (float 리스트)</param>
    public SensorValue(float[] Datas)
    {
        Acceleration = new SensorPoint(Datas[0], Datas[1], Datas[2]);
        AngularVelocity = new SensorPoint(Datas[3], Datas[4], Datas[5]);
        Position = new SensorPoint(Datas[6], Datas[7], Datas[8]);
        Status = SV_Status.NA;
    }
    /// <summary>
    /// 기본 생성자
    /// </summary>
    public SensorValue()
    {
        Acceleration = new SensorPoint(0, 0, 0);
        AngularVelocity = new SensorPoint(0, 0, 0);
        Position = new SensorPoint(0, 0, 0);
        Status = SV_Status.NA;
    }
    /// <summary>
    /// 복사 생성자
    /// </summary>
    /// <param name="other">복사할 다른 개체</param>
    public SensorValue(SensorValue other)
    {
        Acceleration = new SensorPoint(other.Acceleration.x, other.Acceleration.y, other.Acceleration.z);
        AngularVelocity = new SensorPoint(other.AngularVelocity.x, other.AngularVelocity.y, other.AngularVelocity.z);
        Position = new SensorPoint(other.Position.x, other.Position.y, other.Position.z);
        Status = other.Status;
    }
    public static SensorValue operator +(SensorValue A, SensorValue B)
    {
        A.Acceleration += B.Acceleration;
        A.Position += B.Position;
        A.AngularVelocity += B.AngularVelocity;
        return A;
    }
    public static SensorValue operator -(SensorValue A, SensorValue B)
    {
        A.Acceleration -= B.Acceleration;
        A.Position -= B.Position;
        A.AngularVelocity -= B.AngularVelocity;
        return A;
    }
}
public enum SwingSpeedDelta
{
    Slow,
    Acceptable,
    Fast,
}
public enum SwingStrDelta
{ 
    Weak,
    Acceptable,
    Excessive,
}
public struct SwingDeltaData
{
    public SwingSpeedDelta Speed;
    public SwingStrDelta Strength;
    public SwingDeltaData(SwingSpeedDelta nSpeed, SwingStrDelta nStrength)
    {
        Speed = nSpeed;
        Strength = nStrength;
    }
}


[System.Serializable]
public class DataSet
{
    /*
     * 이벤트 선언
     */
    public delegate void DataAdded(SensorValue S);
    public delegate void DataRemoved(SensorValue S);
    public delegate void DataChanged(List<SensorValue> Data);
    public delegate void PVDataAdded(Vector2 P);
    public delegate void PVDataRemoved(Vector2 P);
    public delegate void PVDataChanged(List<Vector2> Data);
    public delegate void DataRefined(List<SensorValue> RefinedData);
    private static DataSet _Instance;
    public static DataSet Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new DataSet();
            return _Instance;
        }
    }
    public static DataAdded ev_data_added;
    public static DataRemoved ev_data_removed;
    public static DataChanged ev_data_changed;
    public static PVDataAdded ev_pvdata_added;
    public static PVDataRemoved ev_pvdata_removed;
    public static PVDataChanged ev_pvdata_changed;
    public static DataRefined ev_data_refined;
    public List<SensorValue> Data;
    [System.NonSerialized]
    public List<SensorValue> Data_Refined;
    public List<float> Power;
    [System.NonSerialized]
    private List<Vector2> _PowerGraphData;
    [System.NonSerialized]
    public static DataSet ComparisonSet;
    [System.NonSerialized]
    public static List<float> LerpedComparisonSet;
    [System.NonSerialized]
    public static List<SwingDeltaData> Deltas;
    public List<Vector2> PowerGraphData
    {
        get
        {
            List<Vector2> Interpolated = RawGraphData;
            /*for (int i = 0; i < Interpolated.Count - 1; i++)
            {
                Interpolated[i] = new Vector2(i,
                    Mathf.Lerp(Interpolated[i].y, Interpolated[i + 1].y, 0.5f));
            }*/
            _PowerGraphData = Interpolated;
            return _PowerGraphData;
        }
    }
    [System.NonSerialized]
    private List<Vector2> _RawGraphData;
    public List<Vector2> RawGraphData
    {
        get
        {
            if (_RawGraphData == null)
                _RawGraphData = new List<Vector2>();
            if (_RawGraphData.Count != Power.Count)
            {
                _RawGraphData.Clear();
                for (int i = 0; i < Power.Count; i++)
                {
                    if (Power[i] >= threshold_static)
                        _RawGraphData.Add(new Vector2(i, Power[i]));
                }
            }
            return _RawGraphData;
        }
    }
    public float SamplingDelay = 0.5f;
    private float _threshold_hit = 150;
    public float threshold_hit
    {
        get
        {
            return _threshold_hit;
        }
        set
        {
            _threshold_hit = value;
            RefineAll();
        }
    }
    public float threshold_static = 10;
    public DataSet()
    {
        Data = new List<SensorValue>();
        Data_Refined = new List<SensorValue>();
        Power = new List<float>();


        // OLD
        /*
         * 0 : x Acceleration
         * 1 : y Acceleration
         * 2 : z Acceleration
         * 3 : x Angular Velocity 
         * 4 : y Angular Velocity
         * 5 : z Angular Velocity
         * 6 : x Position
         * 7 : y Position
         * 8 : z Position
         */
    }
    public static void LoadComparisonData()
    {
        if (ComparisonSet == null)
            ComparisonSet = DataSerializer.ReadFromResource<DataSet>("ComparisonData1");
    }

    public void AddData(SensorValue S)
    {
        Data.Add(S);
        float AccPower = GetAccPower(S);
        Power.Add(AccPower);
        Vector2 PowerData = new Vector2(PowerGraphData.Count, AccPower);
        PowerGraphData.Add(PowerData);

        ev_data_added?.Invoke(S);
        ev_pvdata_added?.Invoke(PowerData);

        ev_data_changed?.Invoke(Data);
        ev_pvdata_changed?.Invoke(PowerGraphData);
    }
    /// <summary>
    /// 현재 데이터를 모두 구간처리 함
    /// </summary>
    public void RefineAll()
    {
        if (Data_Refined == null)
            Data_Refined = new List<SensorValue>();
        Data_Refined.Clear();
        foreach (SensorValue S in Data)
        {
            SensorValue SR = new SensorValue(S);
            float acc_sum = GetAccPower(S);
            bool AddData = false;
            switch (S.Status)
            {
                case SensorValue.SV_Status.PreSwing:
                case SensorValue.SV_Status.AfterSwing:
                    if (acc_sum >= threshold_static)
                        AddData = true;
                    break;
                case SensorValue.SV_Status.SwingHit:
                    AddData = true;
                    break;
            }
            if (AddData)
                Data_Refined.Add(SR);
        }
        ev_data_refined?.Invoke(Data_Refined);
    }
    public void Clear()
    {
        Data.Clear();
        Data_Refined.Clear();
        Power.Clear();
        if (_PowerGraphData != null)
        {
            _PowerGraphData.Clear();
            PowerGraphData.Clear();
        }
    }
    public static float GetAccPower(SensorValue S)
    {
        return Mathf.Sqrt(Mathf.Pow(S.Acceleration.x, 2) + Mathf.Pow(S.Acceleration.y, 2) +
            Mathf.Pow(S.Acceleration.z, 2));
    }

    public bool SaveToFile()
    {
        if (_Instance.Data.Count > 0)
        {
            DataSerializer.SaveToFile(_Instance, "Golf_" + System.DateTime.Now.ToString("MMddyy.Hmmss"));
            return true;
        }
        else
        {
            Debug.LogError("Error : No Data To Save");
            return false;
        }
    }
    public void ReadInstanceFromFile(string filename)
    {
        _Instance = ReadFromFile(filename);
    }
    public DataSet ReadFromFile(string Filename)
    {
        return DataSerializer.ReadFromFile<DataSet>(Filename);
    }
    public float[] CompareAll()
    {
        List<Thread> threads = new List<Thread>();
        System.Action ThreadOp = () =>
        {
            foreach (Thread T in threads)
                T.Start();
            while (true)
            {
                int joincount = 0;
                foreach (Thread T in threads)
                {
                    if (T.ThreadState == ThreadState.Stopped)
                        joincount++;
                }
                if (joincount == threads.Count)
                    break;
            }
            threads.Clear();
        };
        threads.Add(new Thread(() =>
      {
          RefineAll();
      }));
        threads.Add(new Thread(() =>
        {
            ComparisonSet.RefineAll();
        }));
        ThreadOp();

        if (Deltas == null)
            Deltas = new List<SwingDeltaData>();
        else
            Deltas.Clear();
        float[] Zones = new float[3];
        List<float> Usr_PreSwing = new List<float>();
        List<float> Usr_SwingHit = new List<float>();
        List<float> Usr_AfterSwing = new List<float>();
        List<float> Comp_PreSwing = new List<float>();
        List<float> Comp_SwingHit = new List<float>();
        List<float> Comp_AfterSwing = new List<float>();
        System.Action<List<SensorValue>, List<float>, List<float>, List<float>> ExtractZones =
            (datas, list_pre, list_hit, list_after) =>
            {
                foreach (SensorValue S in datas)
                {
                    float AccPow = GetAccPower(S);
                    switch (S.Status)
                    {
                        case SensorValue.SV_Status.PreSwing:
                            list_pre.Add(AccPow);
                            break;
                        case SensorValue.SV_Status.SwingHit:
                            list_hit.Add(AccPow);
                            break;
                        case SensorValue.SV_Status.AfterSwing:
                            list_after.Add(AccPow);
                            break;
                    }
                }
            };
        threads.Add(new Thread(() => ExtractZones(Data_Refined, Usr_PreSwing, Usr_SwingHit, Usr_AfterSwing)));
        threads.Add(new Thread(() => ExtractZones(ComparisonSet.Data_Refined, Comp_PreSwing, Comp_SwingHit, Comp_AfterSwing)));
        ThreadOp();
        List<float> LerpList_A = new List<float>();
        List<float> LerpList_B = new List<float>();
        List<float> LerpList_C = new List<float>();
        SwingDeltaData D1 = new SwingDeltaData();
        SwingDeltaData D2 = new SwingDeltaData();
        SwingDeltaData D3 = new SwingDeltaData();
        threads.Add(new Thread(() => Compare(Usr_PreSwing, Comp_PreSwing, out Zones[0], out LerpList_A, out D1)));
        threads.Add(new Thread(() => Compare(Usr_SwingHit, Comp_SwingHit, out Zones[1], out LerpList_B, out D2)));
        threads.Add(new Thread(() => Compare(Usr_AfterSwing, Comp_AfterSwing, out Zones[2], out LerpList_C, out D3)));
        ThreadOp();
        threads.Add(new Thread(() =>
        {
            if (LerpedComparisonSet == null)
                LerpedComparisonSet = new List<float>();
            else
                LerpedComparisonSet.Clear();
            foreach (float f in LerpList_A)
                LerpedComparisonSet.Add(f);
            foreach (float f in LerpList_B)
                LerpedComparisonSet.Add(f);
            foreach (float f in LerpList_C)
                LerpedComparisonSet.Add(f);
        }));
        ThreadOp();
        Deltas.Add(D1);
        Deltas.Add(D2);
        Deltas.Add(D3);
        return Zones;
    }

    public float Check(int i, int j, int r_size, int c_size, float[,] arr)
    {
        if (i <= r_size && j <= c_size)
        {
            return arr[i, j];
        }
        else
        {
            return 12345678;
        }
    }

    public void Compare(List<float> User, List<float> Comparison, out float ResultSimilarity,
        out List<float> LerpedList, out SwingDeltaData DeltaData)
    {
        SwingDeltaData nDelta = new SwingDeltaData();
        //float Similarity = 0;
        float Stepping = 0f;
        //float SimilarityThres = 75f;
        float DeltaStrSum = 0;

        //추가분
        float xSigma = 0, ySigma = 0;
        float xPowSigma = 0, yPowSigma = 0;
        float multiplyXYSigma = 0;
        float n = 0;

        List<float> User_dtw = new List<float>();
        List<float> Comparison_dtw = new List<float>();

        float[,] arr = new float[User.Count + 1, Comparison.Count + 1];

        arr[0, 0] = 0;
        for (int i = 1; i <= Comparison.Count; i++)
        {
            arr[0, i] = Comparison[i - 1];
        }
        for (int i = 1; i <= User.Count; i++)
        {
            arr[i, 0] = User[i - 1];
        }
        arr[1, 1] = Mathf.Abs(arr[1, 0] - arr[0, 1]);
        for (int i = 2; i <= User.Count; i++)
        {
            arr[i, 1] = Mathf.Abs(arr[i, 0] - arr[0, 1]) + arr[i - 1, 1];
        }
        for (int i = 2; i <= Comparison.Count; i++)
        {
            arr[1, i] = Mathf.Abs(arr[1, 0] - arr[0, i]) + arr[1, i - 1];
        }

        for (int i = 2; i <= User.Count && i <= Comparison.Count; i++)
        {
            arr[i, i] = Mathf.Abs(arr[i, 0] - arr[0, i]) + Mathf.Min(arr[i - 1, i], arr[i - 1, i - 1], arr[i, i - 1]);
            for (int j = i + 1; j <= Comparison.Count; j++)
            {
                arr[i, j] = Mathf.Abs(arr[i, 0] - arr[0, j]) + Mathf.Min(arr[i - 1, j], arr[i - 1, j - 1], arr[i, j - 1]);
            }
            for (int j = i + 1; j <= User.Count; j++)
            {
                arr[j, i] = Mathf.Abs(arr[j, 0] - arr[0, i]) + Mathf.Min(arr[j - 1, i], arr[j - 1, i - 1], arr[j, i - 1]);
            }
        }

        int x = 1;
        int y = 1;
        User_dtw.Add(arr[1, 0]);
        Comparison_dtw.Add(arr[0, 1]);
        while (true)
        {
            float minval = Mathf.Min(Check(x + 1, y + 1, User.Count, Comparison.Count, arr), Check(x, y + 1, User.Count, Comparison.Count, arr), Check(x + 1, y, User.Count, Comparison.Count, arr));
            if (minval == 12345678)
            {
                break;
            }
            else if (minval == Check(x + 1, y + 1, User.Count, Comparison.Count, arr))
            {
                x++;
                y++;
            }
            else if (minval == Check(x, y + 1, User.Count, Comparison.Count, arr))
            {
                y++;
            }
            else
            {
                x++;
            }
            User_dtw.Add(arr[x, 0]);
            Comparison_dtw.Add(arr[0, y]);
        }

        for (int i = 0; i < User_dtw.Count; i++)
        {
            multiplyXYSigma += (User_dtw[i] * Comparison_dtw[i]);
            xSigma += User_dtw[i];
            ySigma += Comparison_dtw[i];
            xPowSigma += (float)Mathf.Pow(User_dtw[i], 2);
            yPowSigma += (float)Mathf.Pow(Comparison_dtw[i], 2);
            n++;
        }
        //여기까지

        int TickDelta = User.Count - Comparison.Count;
        if (System.Math.Abs(TickDelta) < 2)
        {
            nDelta.Speed = SwingSpeedDelta.Acceptable;
        }
        else
        {
            if (TickDelta > 0)
                nDelta.Speed = SwingSpeedDelta.Slow;
            else
                nDelta.Speed = SwingSpeedDelta.Fast;
        }


        Stepping = Comparison.Count / (float)User.Count;
        List<float> LerpedList_N = new List<float>();
        for (int i = 0; i < User.Count; i++)
        {
            float StepValue = Stepping * i;
            int Floor = Mathf.FloorToInt(StepValue);
            int Ceil = Mathf.CeilToInt(StepValue);
            float Delta = StepValue - Floor;
            float LerpValue = 0;
            //float nSimilarity;
            if (Ceil >= Comparison.Count || Floor >= Comparison.Count)
            {
                LerpValue = Comparison[Comparison.Count - 1];
            }
            else if (Floor < 0 || Floor < 0)
            {
                LerpValue = Comparison[0];
            }
            else
            {
                try
                {
                    LerpValue = Mathf.Lerp(Comparison[Floor], Comparison[Ceil], Delta);
                }
                catch (System.Exception E)
                {
                    Debug.LogError("OutOfRange : " + string.Format("Floor {0} | Ceil {1} | Max {2}", Floor, Ceil, Comparison.Count));
                    Debug.LogError(E.Message);
                }
            }
            LerpedList_N.Add(LerpValue);
            /*
            DeltaStrSum += User[i] - LerpValue;
            nSimilarity = (Mathf.Abs(User[i] - LerpValue) / SimilarityThres);
            if (nSimilarity > 1)
                nSimilarity = 0;
            else
                nSimilarity = 1.0f - nSimilarity;
            Similarity += nSimilarity;
            */
            /*
            multiplyXYSigma += (User[i] * LerpValue);
            xSigma += User[i];
            ySigma += LerpValue;
            xPowSigma += (float)Mathf.Pow(User[i], 2);
            yPowSigma += (float)Mathf.Pow(LerpValue, 2);
            n++;
            */

        }
        DeltaStrSum /= User.Count;
        /*Similarity = Similarity / User.Count;
        Similarity *= 100;
        if (Similarity < 0)
            Similarity = 0;*/
        //ResultSimilarity = Similarity;(
        ResultSimilarity = 100 * (((n * multiplyXYSigma) - (xSigma * ySigma)) /
            ((float)Mathf.Sqrt(((n * xPowSigma) - (float)Mathf.Pow(xSigma, 2)) * ((n * yPowSigma)
            - (float)Mathf.Pow(ySigma, 2)))));
        if (ResultSimilarity < 0)
            ResultSimilarity *= -1;
        //ResultSimilarity = 0.0f;
        //if (ResultSimilarity < 85)
        //ResultSimilarity = ResultSimilarity * ((90.0f - ResultSimilarity) / 100.0f);
        LerpedList = LerpedList_N;
        float StrThres = 20;
        if (System.Math.Abs(DeltaStrSum) < StrThres)
        {
            nDelta.Strength = SwingStrDelta.Acceptable;
        }
        else
        {
            if (DeltaStrSum > 0)
                nDelta.Strength = SwingStrDelta.Excessive;
            else
                nDelta.Strength = SwingStrDelta.Weak;
        }

        DeltaData = nDelta;
    }

    /*
    public float compare(DataSet target)
    {
        System.Func<float, float, float> func = (A, B) =>
        {
            float[] sample = new float[3];
            float[] user = new float[3];
            float similarity = 0;
            float overlap = 0;
            float s_length = 0;
            float u_length = 0;

            sample[0] = A.x;
            sample[1] = A.y;
            sample[2] = A.z;

            user[0] = B.x;
            user[1] = B.y;
            user[2] = B.z;

            for (int j = 0; j < 3; j++)
            {
                overlap += Mathf.Sqrt(sample[j] * user[j]);
                s_length += sample[j];
                u_length += user[j];
            }

            similarity = overlap / ((s_length + u_length) / 2);

            return similarity * 100;
        };
    }*/
}

// OLD
/*
public void Refine()
{
    data_refined.Clear();
    bool trigger = false;
    foreach (List<float> target_data in data)
    {
        float acc_sum =
        Mathf.Pow(target_data[6], 2) + Mathf.Pow(target_data[7], 2) + Mathf.Pow(target_data[8], 2);
        if (Mathf.Sqrt(acc_sum) >= 80)
            trigger = true;
        else
            trigger = false;
        if (trigger)
            data_refined.Add(target_data);
        else
            data_refined.Add(null);
    }
}*/
/*
public Vector3 GetAcceleration(int index, bool refined = true)
{
    if (refined)
        return new Vector3(data_refined[index][0], data_refined[index][1], data_refined[index][2]);
    else
        return new Vector3(data[index][0], data[index][1], data[index][2]);
}

public Vector3 GetAngularVelocity(int index, bool refined = true)
{
    if (refined)
        return new Vector3(data_refined[index][3], data_refined[index][4], data_refined[index][5]);
    else
        return new Vector3(data[index][3], data[index][4], data[index][5]);
}

public Vector3 GetPositions(int index, bool refined = true)
{
    if (refined)
        return new Vector3(data_refined[index][6], data_refined[index][7], data_refined[index][8]);
    else
        return new Vector3(data[index][6], data[index][7], data[index][8]);
}
*/
