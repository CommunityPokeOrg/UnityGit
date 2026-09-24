using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityPoke.UnityGit
{
    public abstract class GitObject { public abstract string Type { get; } public abstract byte[] Serialize(); }
    public sealed class Blob : GitObject { public byte[] Data { get; } public Blob(byte[] data) { Data = data ?? Array.Empty<byte>(); } public override string Type => "blob"; public override byte[] Serialize() => Data; }
    public sealed class TreeEntry { public string Mode { get; } public string Name { get; } public string Hash { get; } public TreeEntry(string mode,string name,string hash){Mode=mode;Name=name;Hash=hash;} }
    public sealed class Tree : GitObject {
        public IReadOnlyList<TreeEntry> Entries { get; } public Tree(IEnumerable<TreeEntry> entries){Entries=entries.ToArray();} public override string Type=>"tree";
        public override byte[] Serialize(){var b=new List<byte>(); foreach(var e in Entries){b.AddRange(Encoding.ASCII.GetBytes(e.Mode+" "+e.Name));b.Add(0);b.AddRange(Convert.FromHexString(e.Hash));} return b.ToArray();}
    }
    public sealed class Commit : GitObject {
        public string TreeHash {get;} public IReadOnlyList<string> Parents {get;} public string Author {get;} public string Message {get;}
        public Commit(string treeHash,IEnumerable<string> parents,string author,string message){TreeHash=treeHash;Parents=parents.ToArray();Author=author;Message=message;}
        public override string Type=>"commit";
        public override byte[] Serialize(){var s=new StringBuilder().Append("tree ").Append(TreeHash).Append('\n');foreach(var p in Parents)s.Append("parent ").Append(p).Append('\n');s.Append("author ").Append(Author).Append('\n').Append("committer ").Append(Author).Append("\n\n").Append(Message);return Encoding.UTF8.GetBytes(s.ToString());}
    }
    public sealed class Tag : GitObject { public string TargetHash{get;} public string TargetType{get;} public string Name{get;} public string Tagger{get;} public string Message{get;} public Tag(string hash,string type,string name,string tagger,string message){TargetHash=hash;TargetType=type;Name=name;Tagger=tagger;Message=message;} public override string Type=>"tag"; public override byte[] Serialize()=>Encoding.UTF8.GetBytes("object "+TargetHash+"\ntype "+TargetType+"\ntag "+Name+"\ntagger "+Tagger+"\n\n"+Message); }
}