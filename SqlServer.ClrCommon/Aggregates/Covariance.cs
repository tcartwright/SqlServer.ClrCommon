using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;


/// <summary>
/// Covariance shows the relationship between two sets of numbers.
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined, 
    IsInvariantToDuplicates = false, 
    IsInvariantToNulls = false, 
    IsNullIfEmpty = true, 
    IsInvariantToOrder = true, 
    MaxByteSize = -1
)]
public struct xagCovariance : IBinarySerialize
{
    //http://devnambi.com/2011/t-sql-tuesday-aggregates/

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Init()
    {
        // Put your code here
        this.firstList = new List<double>();
        this.secondList = new List<double>();
        this.sumOfFirst = 0;
        this.sumOfSecond = 0;
    }

    /// <summary>
    /// Accumulates the specified first value.
    /// </summary>
    /// <param name="FirstValue">The first value.</param>
    /// <param name="SecondValue">The second value.</param>
    public void Accumulate(SqlDouble FirstValue, SqlDouble SecondValue)
    {
        // Put your code here
        if (FirstValue.IsNull != true && SecondValue.IsNull != true)
        {
            this.firstList.Add(FirstValue.Value);
            this.sumOfFirst += Convert.ToDecimal(FirstValue.Value);

            this.secondList.Add(SecondValue.Value);
            this.sumOfSecond += Convert.ToDecimal(SecondValue.Value);
        }
    }

    /// <summary>
    /// Merges the specified group.
    /// </summary>
    /// <param name="Group">The group.</param>
    public void Merge(xagCovariance Group)
    {
        // Put your code here
        List<double> firstListToCopy = Group.FirstList;
        List<double> secondListToCopy = Group.SecondList;

        this.sumOfFirst += Group.SumOfFirst;
        this.sumOfSecond += Group.SumOfSecond;

        foreach (double d in firstListToCopy)
        {
            this.firstList.Add(d);
        }

        foreach (double d in secondListToCopy)
        {
            this.secondList.Add(d);
        }
    }

    /// <summary>
    /// Terminates this instance.
    /// </summary>
    /// <returns></returns>
    public SqlDouble Terminate()
    {
        decimal covariance = 0.0M;
        double countOfNumbers = Convert.ToDouble(this.firstList.Count);
        
        decimal firstMean = this.SumOfFirst / Convert.ToDecimal(countOfNumbers);
        decimal secondMean = this.SumOfSecond / Convert.ToDecimal(countOfNumbers);

        decimal firstNumber;
        decimal secondNumber;

        for (int i=0; i < countOfNumbers; i++) 
        {
            firstNumber = Convert.ToDecimal(this.firstList[i]) - firstMean;
            secondNumber = Convert.ToDecimal(this.secondList[i]) - secondMean;

            covariance += firstNumber * secondNumber / Convert.ToDecimal(countOfNumbers);
        }

        return new SqlDouble(Convert.ToDouble(covariance));
    }


    /*
     * The binary layout is as follows:
     * The first 16 bytes are the sumOfFirst decimal
     * The second 16 bytes are the sumOfSecond decimal
     * The third 16 bytes are the product decimal
     * Then, each 8 bytes alternates between the first and second doubles in the lists
     */
    /// <summary>
    /// Reads the specified binary reader.
    /// </summary>
    /// <param name="binaryReader">The binary reader.</param>
    public void Read(BinaryReader binaryReader)
    {
        if (this.firstList == null)
        {
            firstList = new List<double>();
        }

        if (this.secondList == null)
        {
            secondList = new List<double>();
        }

        this.sumOfFirst = binaryReader.ReadDecimal();
        this.sumOfSecond = binaryReader.ReadDecimal();
        
        long readerLength = binaryReader.BaseStream.Length;
        while (binaryReader.BaseStream.Position <= (readerLength - 16))
        {
            this.firstList.Add(binaryReader.ReadDouble());
            this.secondList.Add(binaryReader.ReadDouble());
        }
    }

    /// <summary>
    /// Writes the specified binary writer.
    /// </summary>
    /// <param name="binaryWriter">The binary writer.</param>
    public void Write(BinaryWriter binaryWriter)
    {
        binaryWriter.Write(this.SumOfFirst);
        binaryWriter.Write(this.SumOfSecond);

        int count = this.firstList.Count;
        for (int i = 0; i < count; i++)
        {
            binaryWriter.Write(this.firstList[i]);
            binaryWriter.Write(this.secondList[i]);
        }
    }

    // This is a place-holder member field
    private List<double> firstList;
    private List<double> secondList;
    private decimal sumOfFirst;
    private decimal sumOfSecond;

    /// <summary>
    /// Gets the first list.
    /// </summary>
    /// <value>
    /// The first list.
    /// </value>
    public List<double> FirstList
    {
        get
        {
            return this.FirstList;
        }
    }

    /// <summary>
    /// Gets the second list.
    /// </summary>
    /// <value>
    /// The second list.
    /// </value>
    public List<double> SecondList
    {
        get
        {
            return this.secondList;
        }
    }

    /// <summary>
    /// Gets the sum of first.
    /// </summary>
    /// <value>
    /// The sum of first.
    /// </value>
    public Decimal SumOfFirst
    {
        get { return this.sumOfFirst; } 
    }

    /// <summary>
    /// Gets the sum of second.
    /// </summary>
    /// <value>
    /// The sum of second.
    /// </value>
    public Decimal SumOfSecond
    {
        get { return this.sumOfSecond; }
    }


}
