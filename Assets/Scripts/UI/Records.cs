using System;
using System.Collections.Generic;

public class Records
{
    public int curPosition = 0;
    public int frequency = 10;

    public float length;
    public float[] timeRecord = new float[3];
    public float average = 0;
    public float period = 0;
    public float periodSquare = 0;

    public Records(float length, float time)
    {
        this.length = length;
        timeRecord[curPosition] = time;
        curPosition++;
    }

    public void addRecord(float time)
    {
        timeRecord[curPosition] = time;
        curPosition++;
    }

    public void changeRecord(float time, int position)
    {
        timeRecord[position] = time;
    }

    public void updateVariables(float average, float period, float periodSquare)
    {
        this.average = average;
        this.period = period;
        this.periodSquare = periodSquare;
    }

    public void updateFrequency(int frequency)
    {
        this.frequency = frequency;
    }
}
