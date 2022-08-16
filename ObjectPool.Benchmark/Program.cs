// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ObjectPool.Benchmark;

Console.WriteLine("Hello, ObjectPool Benchmark!");

var summary = BenchmarkRunner.Run<Benchmark>();