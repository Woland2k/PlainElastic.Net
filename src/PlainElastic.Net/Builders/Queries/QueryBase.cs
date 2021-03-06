using System;
using System.Collections.Generic;
using PlainElastic.Net.Builders;
using PlainElastic.Net.Utils;

namespace PlainElastic.Net.Queries
{
    public abstract class QueryBase<TPart> : IJsonConvertible where TPart : QueryBase<TPart>
    {

        private readonly List<string> jsonParts = new List<string>();


        public IEnumerable<string> JsonParts { get { return jsonParts; } }

        protected bool HasCustomPatrs { get; private set; }


        /// <summary>
        /// Adds a custom part to Query or Filter.
        /// </summary>
        public TPart Custom(string partFormat, params string[] args)
        {
            if (partFormat.IsNullOrEmpty())
                return (TPart)this;

            var json = partFormat.F(args);
            AddJsonPart(json);

            HasCustomPatrs = true;
            return (TPart)this;
        }



        protected void RegisterJsonPart(string jsonPart, params string[] args)
        {
            if (jsonPart.IsNullOrEmpty())
                return;

            var json = jsonPart.AltQuoteF(args);
            AddJsonPart(json);
        }

        protected TResultJsonPart RegisterJsonPartExpression<TJsonPart, TResultJsonPart>(Func<TJsonPart, TResultJsonPart> partExpression)
            where TJsonPart : new()
            where TResultJsonPart : IJsonConvertible
        {
            var inputInstance = new TJsonPart();
            var resultPart = partExpression.Invoke(inputInstance);
           
            var json = resultPart.ToJson();

            AddJsonPart(json);

            return resultPart;
        }


        protected abstract bool HasRequiredParts();

        protected abstract string ApplyJsonTemplate(string body);


        public bool GetIsEmpty()
        {
            return ((IJsonConvertible) this).ToJson().IsNullOrEmpty();
        }


        private void AddJsonPart(string json)
        {
            if (json.IsNullOrEmpty())
                return;
            jsonParts.Add(json);
        }


        string IJsonConvertible.ToJson()
        {
            if (jsonParts.Count == 0 || (!HasCustomPatrs && !HasRequiredParts()))
                return "";

            var body = JsonParts.JoinWithComma();
            return ApplyJsonTemplate(body);
        }


        public override string ToString()
        {
            return ((IJsonConvertible) this).ToJson();
        }
        
    }
}